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
                var food = foods[i];
                string query = $"INSERT INTO \"Ingredient\" " +
                                "(\"IngredientName\", \"Calories\", \"Carbohydrate\", \"Sugar\", \"Fiber\", \"Fat\", \"Protein\") " +
                                $"VALUES ('{food.Name}', '{food.Calories}','{food.Carbohydrate}', '{food.Sugar}', '{food.Fiber}', '{food.Fat}', '{food.Protein}') " +
                                "RETURNING *;";
                var insertedFood = await connection.QueryAsync<MyFoodObject>(query);
                insertedFoods.Add(insertedFood.FirstOrDefault()!);
            }

            Console.WriteLine("------------------------");
            Console.WriteLine("Successfully inserted Ingredients");
            return insertedFoods;
        }
    }
}
