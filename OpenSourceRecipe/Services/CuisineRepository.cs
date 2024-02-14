using System.Text;
using Dapper;
using Npgsql;
using OpenSourceRecipes.Models;

namespace OpenSourceRecipes.Services;

public class CuisineRepository
{
    private readonly IConfiguration _configuration;
    private readonly string? _connectionString;

    public CuisineRepository(IConfiguration configuration)
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

    public async Task<IEnumerable<GetCuisineDto>> GetAllCuisines()
    {
        await using var connection = new NpgsqlConnection(_configuration.GetConnectionString(_connectionString!));

        // get all cuisines with count of recipes
        StringBuilder query = new StringBuilder();
        query.Append("SELECT c.\"CuisineId\", c.\"CuisineName\", c.\"CuisineImg\", c.\"Description\", COUNT(r.\"CuisineId\") AS \"RecipeCount\" ");
        query.Append("FROM \"Cuisine\" c ");
        query.Append("LEFT JOIN \"Recipe\" r ON c.\"CuisineId\" = r.\"CuisineId\" ");
        query.Append("GROUP BY c.\"CuisineId\" ");
        query.Append("ORDER BY c.\"CuisineName\" ASC");

        var cuisines = await connection.QueryAsync<GetCuisineDto>(query.ToString());

        return cuisines;
    }
}
