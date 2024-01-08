using Dapper;
using Npgsql;

namespace OpenSourceRecipes.Seeds
{
    public class CommentObject
    {
        public int CommentId { get; set; }
        public string Comment { get; set; } = "";
        public int RecipeId { get; set; } = 0;
        public int UserId { get; set; } = 0;
        public string Author { get; set; } = "";
    }

    public class SeedCommentsData(IConfiguration configuration)
    {
        public async Task<IEnumerable<CommentObject>> InsertIntoComments(string connectionStringName)
        {
            await using var connection = new NpgsqlConnection(configuration.GetConnectionString(connectionStringName));

            var commentsData = new CommentsData();
            CommentObject[] commentArr = commentsData.GetComments();

            List<CommentObject> insertedComments = new List<CommentObject>();

            Console.WriteLine("Inserting Comments");
            Console.WriteLine("------------------------");

            for (int i = 0; i < commentArr.Length; i++)
            {
                try
                {
                    CommentObject comment = commentArr[i];

                    string query = $"INSERT INTO \"RecipeComment\" " +
                                    "(\"Comment\", \"RecipeId\", \"UserId\", \"Author\") " +
                                    "VALUES (@Comment, @RecipeId, @UserId, @Author) " +
                                    "RETURNING *;";

                    var insertedComment = await connection.QueryFirstOrDefaultAsync<CommentObject>(query, comment);
                    if (insertedComment != null) insertedComments.Add(insertedComment);
                }
                catch (Exception ex)
                {
                     Console.WriteLine($"Error inserting comment: {ex}");
                    throw;
                }
            }
            Console.WriteLine("------------------------");
            Console.WriteLine("Successfully inserted Comments");
            return insertedComments;
        }
    }
}
