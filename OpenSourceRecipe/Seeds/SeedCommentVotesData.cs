using Dapper;
using Npgsql;

namespace OpenSourceRecipes.Seeds
{
    public class VoteObject
    {
        public int UserId { get; set; } = 0;
        public int CommentId { get; set; } = 0;
        public bool UpVote { get; set; } = false;
    }

    public class SeedCommentVotesData(IConfiguration configuration)
    {
        public async Task<IEnumerable<VoteObject>> InsertIntoCommentVotes(string connectionStringName)
        {
            await using var connection = new NpgsqlConnection(configuration.GetConnectionString(connectionStringName));

            var votesData = new CommentVotesData();
            VoteObject[] voteArr = votesData.GetVotesData();

            List<VoteObject> insertedVotes = new List<VoteObject>();

            Console.WriteLine("Inserting Votes");
            Console.WriteLine("------------------------");

            for (int i = 0; i < voteArr.Length; i++)
            {
                try
                {
                    VoteObject vote = voteArr[i];

                    string query = $"INSERT INTO \"CommentVote\" " +
                                    "(\"UserId\", \"CommentId\", \"Upvote\") " +
                                    "VALUES (@UserId, @CommentId, @UpVote) " +
                                    "RETURNING *;";

                    var insertedVote = await connection.QueryFirstOrDefaultAsync<VoteObject>(query, vote);
                    if (insertedVote != null) insertedVotes.Add(insertedVote);
                }
                catch (Exception ex)
                {
                     Console.WriteLine($"Error inserting Vote: {ex}");
                    throw;
                }
            }
            Console.WriteLine("------------------------");
            Console.WriteLine("Successfully inserted Votes");
            return insertedVotes;
        }
    }
}
