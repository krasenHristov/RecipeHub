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

    public async Task<IEnumerable<GetIngredientDto>> GetAllIngredients()
    {
        await using var connection = new NpgsqlConnection(_configuration.GetConnectionString(_connectionString!));

        var sql = "SELECT * FROM \"Ingredient\"";
        var ingredients = await connection.QueryAsync<GetIngredientDto>(sql);

        return ingredients;
    }

}
