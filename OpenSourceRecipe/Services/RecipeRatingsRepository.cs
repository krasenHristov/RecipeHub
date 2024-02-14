using System.Text;
using Dapper;
using Npgsql;
using OpenSourceRecipes.Models;

public class RecipeRatingsRepository
{
    private readonly IConfiguration _configuration;
    private readonly string? _connectionString;
        public RecipeRatingsRepository(IConfiguration configuration)
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
                     "(\"RecipeId\", \"UserId\", \"Rating\") " +
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

        StringBuilder query = new StringBuilder();
        query.Append("UPDATE \"RecipeRating\" ");
        query.Append("SET \"Rating\" = @Rating ");
        query.Append("WHERE \"RecipeId\" = @RecipeId AND \"UserId\" = @UserId RETURNING *");

        var newRating = await connection.QuerySingleOrDefaultAsync<RecipeRatingDto>(query.ToString(), parameters);

        if (newRating == null)
        {
            throw new Exception("Failed to update rating.");
        }

        return newRating;
    }

}
