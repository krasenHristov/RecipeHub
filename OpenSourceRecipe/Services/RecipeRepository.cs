using System.Collections;
using Dapper;
using Npgsql;
using OpenSourceRecipes.Models;
using System.Text;

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
                           "(SELECT COUNT(\"RecipeId\") FROM \"Recipe\" WHERE \"OriginalRecipeId\" = r.\"RecipeId\") as \"ForkCount\", " +
                           "COALESCE(ROUND(AVG(rr.\"Rating\")::NUMERIC, 2), 0) as \"AverageRating\", " +
                           "COUNT(rr.\"UserId\") as \"RatingCount\" " +
                           "FROM \"Recipe\" r " +
                           "LEFT JOIN \"RecipeRating\" rr ON r.\"RecipeId\" = rr.\"RecipeId\" " +
                           "WHERE r.\"RecipeId\" = @RecipeId " +
                           "GROUP BY r.\"RecipeId\";";

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
                     "(SELECT COUNT(\"RecipeId\") FROM \"Recipe\" WHERE \"OriginalRecipeId\" = r.\"RecipeId\") as \"ForkCount\", " +
                     "COALESCE(ROUND(AVG(rr.\"Rating\")::NUMERIC, 2), 0) as \"AverageRating\", " +
                     "COUNT(rr.\"UserId\") as \"RatingCount\" " +
                     "FROM \"Recipe\" r " +
                     "LEFT JOIN \"RecipeRating\" rr ON r.\"RecipeId\" = rr.\"RecipeId\" " +
                     "WHERE r.\"ForkedFromId\" IS NULL ";

        DynamicParameters parameters = new DynamicParameters();

        if (userId != null)
        {
            sql += "AND r.\"UserId\" = @UserId ";
            parameters.Add("UserId", userId);
        }

        if (cuisineId != null)
        {
            sql += "AND r.\"CuisineId\" = @CuisineId ";
            parameters.Add("CuisineId", cuisineId);
        }

        sql += " GROUP BY r.\"RecipeId\";";

        var recipes = await connection.QueryAsync<GetRecipesDto>(sql, parameters);

        return recipes;
    }

    public async Task<IEnumerable<GetForkedRecipesDto>> GetForkedRecipes(
        int? userId, int? cuisineId, int? forkedFromId, int? originalRecipeId)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetConnectionString(_connectionString!));

        string sql = "SELECT r.*, " +
                     "(SELECT COUNT(\"RecipeId\") FROM \"Recipe\" WHERE \"ForkedFromId\" = r.\"RecipeId\") as \"DirectForkCount\", " +
                     "COALESCE(ROUND(AVG(rr.\"Rating\")::NUMERIC, 2), 0) as \"AverageRating\", " +
                     "COUNT(rr.\"UserId\") as \"RatingCount\" " +
                     "FROM \"Recipe\" r " +
                     "LEFT JOIN \"RecipeRating\" rr ON r.\"RecipeId\" = rr.\"RecipeId\" " +
                     "WHERE r.\"ForkedFromId\" IS NOT NULL ";

        var parameters = new DynamicParameters();

        if (userId != null)
        {
            sql += "AND r.\"UserId\" = @UserId ";
            parameters.Add("UserId", userId);
        }

        if (cuisineId != null)
        {
            sql += "AND r.\"CuisineId\" = @CuisineId ";
            parameters.Add("CuisineId", cuisineId);
        }

        if (forkedFromId != null)
        {
            sql += "AND r.\"ForkedFromId\" = @ForkedFromId ";
            parameters.Add("ForkedFromId", forkedFromId);
        }

        if (originalRecipeId != null)
        {
            sql += "AND r.\"OriginalRecipeId\" = @OriginalRecipeId ";
            parameters.Add("OriginalRecipeId", originalRecipeId);
        }

        sql += " GROUP BY r.\"RecipeId\";";

        var recipes = await connection.QueryAsync<GetForkedRecipesDto>(sql, parameters);

        return recipes;
    }

    public async Task<GetRecipeByIdDto> PatchRecipe(PatchRecipeDto recipe)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetConnectionString(_connectionString!));

        var parameters = new DynamicParameters();
        var sql = new StringBuilder("UPDATE \"Recipe\" SET ");

        var updates = new List<string>();

        if (recipe.RecipeTitle != null)
        {
            parameters.Add("RecipeTitle", recipe.RecipeTitle);
            updates.Add("\"RecipeTitle\" = @RecipeTitle");
        }
        if (recipe.TagLine != null)
        {
            parameters.Add("TagLine", recipe.TagLine);
            updates.Add("\"TagLine\" = @TagLine");
        }
        if (recipe.Difficulty != null)
        {
            parameters.Add("Difficulty", recipe.Difficulty);
            updates.Add("\"Difficulty\" = @Difficulty");
        }
        if (recipe.TimeToPrepare != null)
        {
            parameters.Add("TimeToPrepare", recipe.TimeToPrepare);
            updates.Add("\"TimeToPrepare\" = @TimeToPrepare");
        }
        if (recipe.RecipeMethod != null)
        {
            parameters.Add("RecipeMethod", recipe.RecipeMethod);
            updates.Add("\"RecipeMethod\" = @RecipeMethod");
        }
        if (recipe.RecipeImg != null)
        {
            parameters.Add("RecipeImg", recipe.RecipeImg);
            updates.Add("\"RecipeImg\" = @RecipeImg");
        }
        if (recipe.Cuisine != null)
        {
            parameters.Add("Cuisine", recipe.Cuisine);
            updates.Add("\"Cuisine\" = @Cuisine");
        }
        if (recipe.CuisineId != null)
        {
            parameters.Add("CuisineId", recipe.CuisineId);
            updates.Add("\"CuisineId\" = @CuisineId");
        }

        sql.Append(string.Join(", ", updates));

        parameters.Add("RecipeId", recipe.RecipeId);
        sql.Append(" WHERE \"RecipeId\" = @RecipeId RETURNING *;");

        var updatedRecipe = await connection.QuerySingleOrDefaultAsync<GetRecipeByIdDto>(sql.ToString(), parameters);
        if (updatedRecipe == null)
        {
            throw new Exception("Failed to update recipe.");
        }

        return updatedRecipe;
    }

    public async Task<IEnumerable<GetRecipesDto>> SearchRecipes(string searchTerm)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetConnectionString(_connectionString!));

        string[] searchTerms = searchTerm.Split(' ');

        for (int i = 0; i < searchTerms.Length; i++)
        {
            searchTerms[i] = searchTerms[i] + ":*";
        }
        searchTerm = string.Join(" & ", searchTerms);

        string sql = @"
            SELECT r.*,
                   (SELECT COUNT(""RecipeId"") FROM ""Recipe"" WHERE ""ForkedFromId"" = r.""RecipeId"") as ""DirectForkCount"",
                   COALESCE(ROUND(AVG(rr.""Rating"")::NUMERIC, 2), 0) as ""AverageRating"",
                   COUNT(rr.""UserId"") as ""RatingCount"",
                   ts_rank(""TsvDescription"", to_tsquery('english', @SearchTerm)) as rank
            FROM ""Recipe"" r
            LEFT JOIN ""RecipeRating"" rr ON r.""RecipeId"" = rr.""RecipeId""
            WHERE ""TsvDescription"" @@ to_tsquery('simple', @SearchTerm)
            GROUP BY r.""RecipeId""
            ORDER BY rank DESC
            ";


        return await connection.QueryAsync<GetRecipesDto>(sql, new {SearchTerm = searchTerm});
    }

    public async Task<IEnumerable<GetRecipesDto>> GetRecipesByIngredients(int[] ingredientIds)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetConnectionString(_connectionString!));

        string query = @"
            SELECT ri.""RecipeId""
            FROM ""RecipeIngredient"" ri
            WHERE ri.""RecipeId"" IN (
            SELECT ri_sub.""RecipeId""
            FROM ""RecipeIngredient"" ri_sub
            WHERE ri_sub.""IngredientId"" = ANY(@Ids)
            GROUP BY ri_sub.""RecipeId""
            )
            GROUP BY ri.""RecipeId"";";

        var result = await connection.QueryAsync<int>(query, new { Ids = ingredientIds.ToArray() });

        // get all recipes for these IDs
        string sql = @"
           SELECT
            r.*,
                (SELECT COUNT(""RecipeId"") FROM ""Recipe"" WHERE ""ForkedFromId"" = r.""RecipeId"") as ""DirectForkCount"",
                COALESCE(ROUND(AVG(rr.""Rating"")::NUMERIC, 2), 0) as ""AverageRating"",
                COUNT(rr.""UserId"") as ""RatingCount""
            FROM
                ""Recipe"" r
            LEFT JOIN
                ""RecipeRating"" rr ON r.""RecipeId"" = rr.""RecipeId""
            WHERE
                r.""RecipeId"" = ANY(@Ids)
            GROUP BY
                r.""RecipeId"";
            ";

        return await connection.QueryAsync<GetRecipesDto>(sql, new { Ids = result });
    }
}



