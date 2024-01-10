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
            var cuisineObj = new CuisinesData();
            var cuisineArr = cuisineObj.GetCuisinesData();

            List<MyCuisineObject> insertedCuisines = new List<MyCuisineObject>();

            Console.WriteLine("Inserting Cuisines");
            Console.WriteLine("------------------------");

            foreach (var cuisine in cuisineArr)
            {
                try
                {
                    string query = "INSERT INTO \"Cuisine\" " +
                                   "(\"CuisineId\", \"CuisineName\", \"Description\", \"CuisineImg\") " +
                                   "VALUES (@CuisineId, @CuisineName, @Description, @CuisineImg) " +
                                   "RETURNING *;";

                    var insertedCuisine = await connection.QueryFirstOrDefaultAsync<MyCuisineObject>(
                        query,
                        new { CuisineId = cuisine.CuisineId, CuisineName = cuisine.CuisineName, Description = cuisine.Description, CuisineImg = cuisine.CuisineImg });
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
