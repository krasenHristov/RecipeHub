using Dapper;
using Npgsql;
using OpenSourceRecipes.Models;

namespace OpenSourceRecipes.Services;
public class RecipeRepository
{
    private readonly IConfiguration _configuration;
    private readonly string? _connectionString;
        public RecipeRepository(IConfiguration configuration)
    {
        this._configuration = configuration;

        string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        if (env == "Testing")
        {
            _connectionString = "TestConnection";
        }
        else if (env == "Production")
        {
            _connectionString = "ProductionConnection";
        }
        else
        {
            _connectionString = "DefaultConnection";
        }
    }

    public async Task DeleteRecipe(int recipeId)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetConnectionString(_connectionString!));

        var sql = "DELETE FROM \"Recipe\" WHERE \"RecipeId\" = @RecipeId";

        await connection.ExecuteAsync(sql, new {RecipeId = recipeId});
    }

    public async Task<GetRecipeByIdDto?> GetRecipeById(int recipeId)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetConnectionString(_connectionString!));

        string recipeSql = "SELECT r.*, " +
                           "(SELECT COUNT(\"RecipeId\") FROM \"Recipe\" WHERE \"ForkedFromId\" = r.\"RecipeId\") as \"DirectForkCount\", " +
                           "(SELECT COUNT(\"RecipeId\") FROM \"Recipe\" WHERE \"OriginalRecipeId\" = r.\"RecipeId\") as \"ForkCount\" " +
                           "FROM \"Recipe\" r " +
                           "WHERE \"RecipeId\" = @RecipeId";

        var recipe = await connection.QueryFirstOrDefaultAsync<GetRecipeByIdDto>(recipeSql, new {RecipeId = recipeId});

        var ingredientsSql = "SELECT i.*, ri.\"Quantity\" FROM \"RecipeIngredient\" ri " +
                             "JOIN \"Ingredient\" i " +
                             "ON ri.\"IngredientId\" = i.\"IngredientId\"" +
                             "WHERE \"RecipeId\" = @RecipeId " +
                             "GROUP BY i.\"IngredientId\", ri.\"Quantity\"";

        var ingredients = await connection.QueryAsync<IngredientRecipeDto>(ingredientsSql, new {RecipeId = recipeId});

        if (recipe != null)
        {
            recipe.RecipeIngredients = ingredients.ToList();
            return recipe;
        }

        return null;
    }

    public async Task<GetRecipesDto> CreateRecipe(CreateRecipeDto recipe)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetConnectionString(_connectionString!));

        var parameters = new DynamicParameters();
        parameters.Add("RecipeTitle", recipe.RecipeTitle);
        parameters.Add("TagLine", recipe.TagLine);
        parameters.Add("Difficulty", recipe.Difficulty);
        parameters.Add("TimeToPrepare", recipe.TimeToPrepare);
        parameters.Add("RecipeMethod", recipe.RecipeMethod);
        parameters.Add("RecipeImg", recipe.RecipeImg);
        parameters.Add("Cuisine", recipe.Cuisine);
        parameters.Add("ForkedFromId", recipe.ForkedFromId);
        parameters.Add("OriginalRecipeId", recipe.OriginalRecipeId);
        parameters.Add("UserId", recipe.UserId);
        parameters.Add("CuisineId", recipe.CuisineId);

        var sql = "INSERT INTO \"Recipe\" " +
                  "(\"RecipeTitle\", \"TagLine\", \"Difficulty\", \"TimeToPrepare\", \"RecipeMethod\", \"RecipeImg\", \"Cuisine\", \"ForkedFromId\", \"OriginalRecipeId\", \"UserId\", \"CuisineId\") " +
                  "VALUES (@RecipeTitle, @TagLine, @Difficulty, @TimeToPrepare, @RecipeMethod, @RecipeImg, @Cuisine, @ForkedFromId, @OriginalRecipeId, @UserId, @CuisineId) RETURNING *";

        var newRecipe = await connection.QuerySingleOrDefaultAsync<GetRecipesDto>(sql, parameters);

        if (newRecipe == null)
        {
            throw new Exception("Recipe not created due to missing parameters");
        }

        return newRecipe;
    }

    public async Task<IEnumerable<GetRecipesDto>> GetAllRecipes(int? userId, int? cuisineId)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetConnectionString(_connectionString!));

        string sql = "SELECT r.*, " +
                     "(SELECT COUNT(\"RecipeId\") FROM \"Recipe\" WHERE \"ForkedFromId\" = r.\"RecipeId\") as \"DirectForkCount\", " +
                     "(SELECT COUNT(\"RecipeId\") FROM \"Recipe\" WHERE \"OriginalRecipeId\" = r.\"RecipeId\") as \"ForkCount\" " +
                     "FROM \"Recipe\" r " +
                     "WHERE r.\"ForkedFromId\" IS NULL AND r.\"OriginalRecipeId\" IS NULL ";

        DynamicParameters parameters = new DynamicParameters();

        if (userId != null)
        {
            sql += "AND \"UserId\" = @UserId ";
            parameters.Add("UserId", userId);
        }

        if (cuisineId != null)
        {
            sql += "AND \"CuisineId\" = @CuisineId";
            parameters.Add("CuisineId", cuisineId);
        }

        var recipes = await connection.QueryAsync<GetRecipesDto>(sql, parameters);

        return recipes;
    }

    public async Task<IEnumerable<GetForkedRecipesDto>> GetForkedRecipes(
        int? userId, int? cuisineId, int? forkedFromId, int? originalRecipeId)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetConnectionString(_connectionString!));

        string sql = "SELECT r.*, " +
                     "(SELECT COUNT(\"RecipeId\") FROM \"Recipe\" WHERE \"ForkedFromId\" = r.\"RecipeId\") as \"DirectForkCount\" " +
                     "FROM \"Recipe\" r " +
                     "WHERE r.\"ForkedFromId\" IS NOT NULL ";

        var parameters = new DynamicParameters();

        if (userId != null)
        {
            sql += "AND \"UserId\" = @UserId ";
            parameters.Add("UserId", userId);
        }

        if (cuisineId != null)
        {
            sql += "AND \"CuisineId\" = @CuisineId ";
            parameters.Add("CuisineId", cuisineId);
        }

        if (forkedFromId != null)
        {
            sql += "AND \"ForkedFromId\" = @ForkedFromId ";
            parameters.Add("ForkedFromId", forkedFromId);
        }

        if (originalRecipeId != null)
        {
            sql += "AND \"OriginalRecipeId\" = @OriginalRecipeId ";
            parameters.Add("OriginalRecipeId", originalRecipeId);
        }

        var recipes = await connection.QueryAsync<GetForkedRecipesDto>(sql, parameters);

        return recipes;
    }

    public async Task<RecipeRatingDto> CreateRecipeRating(RecipeRatingDto ratingDetails)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetConnectionString(_connectionString!));

        if (ratingDetails.Rating < 1 || ratingDetails.Rating > 5)
        {
            throw new Exception("Rating is invalid.");
        }

        var parameters = new DynamicParameters(); 
        parameters.Add("RecipeId", ratingDetails.RecipeId);
        parameters.Add("UserId", ratingDetails.UserId);
        parameters.Add("Rating", ratingDetails.Rating);

        string sql = "INSERT INTO \"RecipeRating\" " +
                     "(\"RecipeId\", \"UserId\", \"RATING\") " +
                     "VALUES (@RecipeId, @UserId, @Rating) RETURNING *";

        var newRating = await connection.QuerySingleOrDefaultAsync<RecipeRatingDto>(sql, parameters);

        if (newRating == null)
        {
            throw new Exception("Failed to create new rating. Recipe is invalid or user rating already exists.");
        }

        return newRating;
    }

    public async Task<RecipeRatingDto> UpdateRecipeRating(RecipeRatingDto ratingDetails)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetConnectionString(_connectionString!));

        if (ratingDetails.Rating < 1 || ratingDetails.Rating > 5)
        {
            throw new Exception("Rating is invalid.");
        }

        var parameters = new DynamicParameters(); 
        parameters.Add("RecipeId", ratingDetails.RecipeId);
        parameters.Add("UserId", ratingDetails.UserId);
        parameters.Add("Rating", ratingDetails.Rating);

        string sql = "UPDATE \"RecipeRating\" " +
                     "SET \"RATING\" = @Rating " +
                     "WHERE \"RecipeId\" = @RecipeId AND \"UserId\" = @UserId RETURNING *";

        var newRating = await connection.QuerySingleOrDefaultAsync<RecipeRatingDto>(sql, parameters);

        if (newRating == null)
        {
            throw new Exception("Failed to update rating.");
        }

        return newRating;
    }
}
