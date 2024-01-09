using Dapper;
using Npgsql;
using OpenSourceRecipes.Models;

namespace OpenSourceRecipes.Services;
public class IngredientRepository
{
    private readonly IConfiguration _configuration;
    private readonly string? _connectionString;

    public IngredientRepository(IConfiguration configuration)
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

    public async Task<GetIngredientDto> CreateIngredient(CreateIngredientDto ingredient)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetConnectionString(_connectionString!));

        var parameters = new DynamicParameters();
        parameters.Add("IngredientName", ingredient.IngredientName);
        parameters.Add("Calories", ingredient.Calories);
        parameters.Add("Carbohydrate", ingredient.Carbohydrate);
        parameters.Add("Sugar", ingredient.Sugar);
        parameters.Add("Fiber", ingredient.Fiber);
        parameters.Add("Fat", ingredient.Fat);
        parameters.Add("Protein", ingredient.Protein);

        var sql = "INSERT INTO \"Ingredient\" " +
                  "(\"IngredientName\", \"Calories\", \"Carbohydrate\", \"Sugar\", \"Fiber\", \"Fat\", \"Protein\") " +
                  "VALUES (@IngredientName, @Calories, @Carbohydrate, @Sugar, @Fiber, @Fat, @Protein) RETURNING *";
        var newIngredient = await connection.QuerySingleOrDefaultAsync<GetIngredientDto>(sql, parameters);

        if (newIngredient == null)
        {
            throw new Exception("Ingredient not created due to missing parameters");
        }

        return newIngredient;
    }

    public async Task<IEnumerable<GetAllIngredientsDto>> GetAllIngredients()
    {
        await using var connection = new NpgsqlConnection(_configuration.GetConnectionString(_connectionString!));

        var sql = "SELECT \"IngredientId\", \"IngredientName\" FROM \"Ingredient\" ORDER BY \"IngredientId\" ASC";
        var ingredients = await connection.QueryAsync<GetAllIngredientsDto>(sql);

        return ingredients;
    }

    public async Task<GetRecipeByIdDto?> AddIngredientsToRecipe(int recipeId, int[] ingredientIds, string[] quantity)
    {
        for (int i = 0;i < ingredientIds.Length; i++)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetConnectionString(_connectionString!));

            var parameters = new DynamicParameters();
            parameters.Add("RecipeId", recipeId);
            parameters.Add("IngredientId", ingredientIds[i]);
            parameters.Add("Quantity", quantity[i]);

            var sql = "INSERT INTO \"RecipeIngredient\" " +
                      "(\"RecipeId\", \"IngredientId\", \"Quantity\") " +
                      "VALUES (@RecipeId, @IngredientId, @Quantity) RETURNING *";
            var newRecipeIngredient = await connection.QuerySingleOrDefaultAsync<GetRecipeByIdDto>(sql, parameters);

            if (newRecipeIngredient == null)
            {
                throw new Exception("RecipeIngredient not created due to missing parameters");
            }
        }

        var recipeRepo = new RecipeRepository(_configuration);
        GetRecipeByIdDto? newRecipe = await recipeRepo.GetRecipeById(recipeId);

        return newRecipe;
    }

    public async Task UpdateIngredientsForRecipe(int recipeId, int[] ingredientIds, string[] quantities)
    {
await using var connection = new NpgsqlConnection(_configuration.GetConnectionString(_connectionString!));

        var parameters = new DynamicParameters();
        parameters.Add("RecipeId", recipeId);

        var sql = "DELETE FROM \"RecipeIngredient\" WHERE \"RecipeId\" = @RecipeId";
        await connection.ExecuteAsync(sql, parameters);

        for (int i = 0; i < ingredientIds.Length; i++)
        {
            parameters = new DynamicParameters();
            parameters.Add("RecipeId", recipeId);
            parameters.Add("IngredientId", ingredientIds[i]);
            parameters.Add("Quantity", quantities[i]);

            sql = "INSERT INTO \"RecipeIngredient\" " +
                  "(\"RecipeId\", \"IngredientId\", \"Quantity\") " +
                  "VALUES (@RecipeId, @IngredientId, @Quantity) RETURNING *";
            var newRecipeIngredient = await connection.QuerySingleOrDefaultAsync<GetRecipeByIdDto>(sql, parameters);

            if (newRecipeIngredient == null)
            {
                throw new Exception("RecipeIngredient not created due to missing parameters");
            }

            return;
        }
    }
}
