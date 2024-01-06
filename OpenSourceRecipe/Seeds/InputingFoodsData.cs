using OpenSourceRecipes.Utils;
using Dapper;
using Npgsql;

namespace OpenSourceRecipes.Seeds
{
    public class SeedFoodData(IConfiguration configuration)
    {
        public async Task<List<MyFoodObject>> InsertIntoFood()
        {
            await using var connection = new NpgsqlConnection(configuration.GetConnectionString("TestConnection"));
            ReadFoodFunc readFood = new ReadFoodFunc();

            List<MyFoodObject> foods = readFood.ReadFoodFile();
            List<MyFoodObject> insertedFoods = new List<MyFoodObject>();

            Console.WriteLine("Inserting Ingredients");
            Console.WriteLine("------------------------");

            for (int i = 0; i < foods.Count; i++)
            {
                try
                {
                    var food = foods[i];

                    // Check if the ingredient already exists
                    string checkQuery = "SELECT * FROM \"Ingredient\" WHERE \"IngredientName\" = @Name;";
                    var existingFood = await connection.QueryAsync<MyFoodObject>(checkQuery, new { Name = food.Name });

                    // If the ingredient does not exist, insert it
                    if (!existingFood.Any())
                    {
                        string insertQuery = @"INSERT INTO ""Ingredient""
                                    (""IngredientName"", ""Calories"", ""Carbohydrate"", ""Sugar"", ""Fiber"", ""Fat"", ""Protein"")
                                    VALUES (@Name, @Calories, @Carbohydrate, @Sugar, @Fiber, @Fat, @Protein)
                                    RETURNING *;";
                        var insertedFood = await connection.QueryAsync<MyFoodObject>(insertQuery, food);
                        insertedFoods.Add(insertedFood.FirstOrDefault());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to insert ingredient");
                    Console.WriteLine(e);
                    throw;
                }
            }


            Console.WriteLine("------------------------");
            Console.WriteLine("Successfully inserted" + insertedFoods.Count + "Ingredients");
            return insertedFoods;
        }
    }
}
