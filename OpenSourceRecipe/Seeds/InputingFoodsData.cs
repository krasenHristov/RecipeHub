using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
            ReadFoodFunc ReadFood = new ReadFoodFunc();

            List<MyFoodObject> Foods = ReadFood.ReadFoodFile();
            List<MyFoodObject> insertedFoods = new List<MyFoodObject>();

            Console.WriteLine("about to insert into Foods");
            Console.WriteLine("------------------------");

            foreach (var food in Foods)
            {   
                string query = $"INSERT INTO \"Ingredient\" " +
                                "(\"IngredientId\", \"IngredientName\", \"Calories\", \"Carbohydrate\", \"Sugar\", \"Fiber\", \"Fat\", \"Protein\") " +
                                $"VALUES ('{food.id}', '{food.name}', '{food.calories}','{food.carbohydrate}', '{food.sugar}', '{food.fiber}', '{food.fat}', '{food.protein}') " +
                                "RETURNING *;";
                var insertedfood = await connection.QueryFirstOrDefaultAsync<MyFoodObject>(query);
                insertedFoods.Add(insertedfood);
            }
            return insertedFoods;
        }
    }
}