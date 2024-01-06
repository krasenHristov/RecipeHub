using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Npgsql;

namespace OpenSourceRecipes.Seeds
{

    public class MyCuisineObject
    {
    public int CuisineId { get; set; }
    public string CuisineName { get; set; } = "";
    public string Description { get; set; } = "";
    public string CuisineImg { get; set; } = "";
    }

    public class SeedCuisineData(IConfiguration configuration)
    {
        public async Task<IEnumerable<MyCuisineObject>> InsertIntoCuisine(string connectionStringName)
        {
            await using var connection = new NpgsqlConnection(configuration.GetConnectionString(connectionStringName));
            var cuisineArr = new[]
            {
                new MyCuisineObject
                {
                    CuisineId = 1,
                    CuisineName = "Italian",
                    Description = "Delicious Italian dishes",
                    CuisineImg = "italian.jpg"
                },
                new MyCuisineObject
                {
                    CuisineId = 2,
                    CuisineName = "Mexican",
                    Description = "Spicy Mexican cuisine",
                    CuisineImg = "mexican.jpg"
                },
                new MyCuisineObject
                {
                    CuisineId = 3,
                    CuisineName = "Japanese",
                    Description = "Authentic Japanese flavors",
                    CuisineImg = "japanese.jpg"
                }
            };

            List<MyCuisineObject> insertedCuisines = new List<MyCuisineObject>();

            Console.WriteLine("Inserting Cuisines");
            Console.WriteLine("------------------------");

            foreach (var cuisine in cuisineArr)
            {
                try
                {
                    string query = $"INSERT INTO \"Cuisine\" " +
                                    "(\"CuisineId\", \"CuisineName\", \"Description\", \"CuisineImg\") " +
                                    $"VALUES ('{cuisine.CuisineId}', '{cuisine.CuisineName}', '{cuisine.Description}', '{cuisine.CuisineImg}') " +
                                    "RETURNING *;";
                    var insertedCuisine = await connection.QueryFirstOrDefaultAsync<MyCuisineObject>(query);
                    if (insertedCuisine != null) insertedCuisines.Add(insertedCuisine);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to insert cuisine");
                    Console.WriteLine(e);
                    throw;
                }
            }

            Console.WriteLine("Successfully inserted " + insertedCuisines.Count + " Cuisines");
            Console.WriteLine("------------------------");

            return insertedCuisines;
        }
    }
}
