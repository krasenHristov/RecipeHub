using Dapper;
using Npgsql;

namespace OpenSourceRecipes.Seeds
{
    public class RecipeRatingObject
    {
        public int UserId { get; set; } = 0;
        public int RecipeId { get; set; } = 0;
        public int Rating { get; set; } = 0;
    }

    public class SeedRecipeRatingData(IConfiguration configuration)
    {
        public async Task<IEnumerable<RecipeRatingObject>> InsertIntoRatings(string connectionStringName)
        {
            await using var connection = new NpgsqlConnection(configuration.GetConnectionString(connectionStringName));

            var ratingsObject = new RecipeRatingData();
            var ratingArr = ratingsObject.GetRatingsData();

            List<RecipeRatingObject> insertedRatings = new List<RecipeRatingObject>();

            Console.WriteLine("Inserting Ratings");
            Console.WriteLine("------------------------");

            for (int i = 0; i < ratingArr.Length; i++)
            {
                try
                {
                    RecipeRatingObject recipe = ratingArr[i];
                    string query = $"INSERT INTO \"RecipeRating\" " +
                                    "(\"UserId\", \"RecipeId\", \"Rating\") " +
                                    "VALUES (@UserId, @RecipeId, @Rating) " +
                                    "RETURNING *;";

                    var rating = await connection.QueryFirstOrDefaultAsync<RecipeRatingObject>(query, recipe);
                    if (rating != null) insertedRatings.Add(rating);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error inserting ratings: {ex}");
                    throw;
                }
            }
            Console.WriteLine("------------------------");
            Console.WriteLine("Successfully inserted Ratings");
            return insertedRatings;
        }
    }
}
