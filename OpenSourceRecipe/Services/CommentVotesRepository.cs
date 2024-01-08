using Dapper;
using Npgsql;
using OpenSourceRecipes.Models;

namespace OpenSourceRecipes.Services;

public class CommentVotesRepository
{
    private readonly IConfiguration _configuration;
    private readonly string? _connectionString;

    public CommentVotesRepository(IConfiguration configuration)
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

    public async Task<string> HandleCommentVote(int commentId, int userId, CommentVoteDto voteDetails)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetConnectionString(_connectionString!));

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("CommentId", commentId);
        parameters.Add("UserId", userId);

        string getVoteQuery = "SELECT * FROM \"CommentVote\" WHERE \"CommentId\" = @CommentId AND \"UserId\" = @UserId";

        CommentVoteDto? existingVote = await connection.QueryFirstOrDefaultAsync<CommentVoteDto>(getVoteQuery, parameters);

        if (existingVote != null && existingVote.Upvote && voteDetails.Upvote)
        {
            string deleteVoteQuery = "DELETE FROM \"CommentVote\" WHERE \"CommentId\" = @CommentId AND \"UserId\" = @UserId";

            await connection.ExecuteAsync(deleteVoteQuery, parameters);

            return "Upvote removed.";
        }

        if (existingVote != null && !existingVote.Upvote && !voteDetails.Upvote)
        {
            string deleteVoteQuery = "DELETE FROM \"CommentVote\" WHERE \"CommentId\" = @CommentId AND \"UserId\" = @UserId";

            await connection.ExecuteAsync(deleteVoteQuery, parameters);

            return "Down-vote removed.";
        }

        if (existingVote != null && existingVote.Upvote && !voteDetails.Upvote)
        {
            string updateVoteQuery = "UPDATE \"CommentVote\" SET \"Upvote\" = false WHERE \"CommentId\" = @CommentId AND \"UserId\" = @UserId";

            await connection.ExecuteAsync(updateVoteQuery, parameters);

            return "Down-vote added.";
        }

        if (existingVote != null && !existingVote.Upvote && voteDetails.Upvote)
        {
            string updateVoteQuery = "UPDATE \"CommentVote\" SET \"Upvote\" = true WHERE \"CommentId\" = @CommentId AND \"UserId\" = @UserId";

            await connection.ExecuteAsync(updateVoteQuery, parameters);

            return "Upvote added.";
        }

        if (existingVote == null)
        {
             parameters.Add("Upvote", voteDetails.Upvote);

             string sql = "INSERT INTO \"CommentVote\" " +
                          "(\"CommentId\", \"UserId\", \"Upvote\") " +
                          "VALUES (@CommentId, @UserId, @Upvote)";

             await connection.ExecuteAsync(sql, parameters);
        }

        return "Upvote added.";
    }
}
