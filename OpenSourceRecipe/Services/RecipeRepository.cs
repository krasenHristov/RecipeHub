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
    public async Task<GetRecipeDto> CreateRecipe(CreateRecipeDto recipe)
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
        var newRecipe = await connection.QuerySingleOrDefaultAsync<GetRecipeDto>(sql, parameters);

        if (newRecipe == null)
        {
            throw new Exception("Recipe not created due to missing parameters");
        }

        return newRecipe;
    }

    public async Task<IEnumerable<GetRecipeDto>> GetAllRecipes()
    {
        await using var connection = new NpgsqlConnection(_configuration.GetConnectionString(_connectionString!));

        var sql = "SELECT * FROM \"Recipe\"";

        var recipes = await connection.QueryAsync<GetRecipeDto>(sql);

        return recipes;
    }
}
