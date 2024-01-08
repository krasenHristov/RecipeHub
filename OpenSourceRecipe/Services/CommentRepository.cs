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

    public async Task<IEnumerable<GetCommentDto>> GetCommentsByRecipeId(int recipeId, int userId)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetConnectionString(_connectionString!));

        string recipeQuery = $"SELECT * FROM \"Recipe\" WHERE \"RecipeId\" = @RecipeId;";

        var recipe = await connection.QueryFirstOrDefaultAsync<GetRecipeByIdDto>(recipeQuery, new { RecipeId = recipeId });

        if (recipe == null)
        {
            throw new Exception("Recipe does not exist");
        }

        string query = @"
            SELECT
            rc.*,
            v.""Upvotes"",
            v.""Downvotes"",
            COALESCE(ucv.""CurrentUserVote"", 'none') AS ""CurrUserVote""
            FROM
            ""RecipeComment"" rc
            LEFT JOIN
            (SELECT
                ""CommentId"",
                COUNT(CASE WHEN ""Upvote"" = true THEN 1 END) AS ""Upvotes"",
                COUNT(CASE WHEN ""Upvote"" = false THEN 1 END) AS ""Downvotes""
             FROM
                ""CommentVote""
             GROUP BY ""CommentId"") v ON rc.""CommentId"" = v.""CommentId""
            LEFT JOIN
            (SELECT
                ""CommentId"",
                CASE WHEN ""Upvote"" IS TRUE THEN 'up'
                     WHEN ""Upvote"" IS FALSE THEN 'down'
                END AS ""CurrentUserVote""
             FROM
                ""CommentVote""
             WHERE
                ""UserId"" = @UserId) ucv ON rc.""CommentId"" = ucv.""CommentId""
            WHERE
            rc.""RecipeId"" = @RecipeId;";

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("RecipeId", recipeId);
        parameters.Add("UserId", userId);

        return await connection.QueryAsync<GetCommentDto>(query, parameters);
    }

    public async Task DeleteComment(int commentId)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetConnectionString(_connectionString!));

        string query = $"DELETE FROM \"RecipeComment\" WHERE \"CommentId\" = @CommentId;";

        await connection.ExecuteAsync(query, new { CommentId = commentId });

        return;
    }

    public async Task<GetCommentDto?> GetCommentById(int commentId)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetConnectionString(_connectionString!));

        string query = $"SELECT * FROM \"RecipeComment\" WHERE \"CommentId\" = @CommentId;";

        return await connection.QueryFirstOrDefaultAsync<GetCommentDto>(query, new { CommentId = commentId });
    }

    public async Task<GetCommentDto?> UpdateComment(int commentId, EditCommentBodyDto comment)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetConnectionString(_connectionString!));

        string query = $"UPDATE \"RecipeComment\" " +
                        "SET \"Comment\" = @Comment " +
                        "WHERE \"CommentId\" = @CommentId " +
                        "RETURNING *;";

        DynamicParameters parameters = new DynamicParameters(comment);
        parameters.Add("CommentId", commentId);

        return await connection.QueryFirstOrDefaultAsync<GetCommentDto>(query, parameters);
    }
}
