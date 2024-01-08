using Dapper;
using Npgsql;
using OpenSourceRecipes.Models;

namespace OpenSourceRecipes.Services;
public class CommentRepository
{
    private readonly IConfiguration _configuration;
    private readonly string? _connectionString;

    public CommentRepository(IConfiguration configuration)
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

    public async Task<GetCommentDto?> CreateComment(CreateCommentDto comment)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetConnectionString(_connectionString!));

        string query = $"INSERT INTO \"RecipeComment\" " +
                        "(\"RecipeId\", \"UserId\", \"Author\", \"Comment\") " +
                        "VALUES (@RecipeId, @UserId, @Author, @Comment) " +
                        "RETURNING *;";

        return await connection.QueryFirstOrDefaultAsync<GetCommentDto>(query, comment);
    }

    public async Task<IEnumerable<GetCommentDto>> GetCommentsByRecipeId(int recipeId)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetConnectionString(_connectionString!));

        string recipeQuery = $"SELECT * FROM \"Recipe\" WHERE \"RecipeId\" = {recipeId};";

        if (await connection.QueryFirstOrDefaultAsync<GetRecipeByIdDto>(recipeQuery) == null)
        {
            throw new Exception("Recipe does not exist");
        }

        string query = $"SELECT * FROM \"RecipeComment\" WHERE \"RecipeId\" = {recipeId};";

        return await connection.QueryAsync<GetCommentDto>(query);
    }
}
