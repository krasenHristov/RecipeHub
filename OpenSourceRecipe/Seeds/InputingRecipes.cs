using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Npgsql;

namespace OpenSourceRecipes.Seeds
{
    public class MyRecipeObject
    {
    public int RecipeId { get; set; } = 0;
    public string RecipeTitle { get; set; } = "";
    public string TagLine { get; set; } = "";
    public int Difficulty { get; set; } = 0;
    public int TimeToPrepare { get; set; } = 0;
    public string RecipeMethod { get; set; } = "";
    public string PostedOn { get; set; } = "";
    public int? ForkedFromId { get; set; } = null;
    public string Cuisine { get; set; } = "";
    public string RecipeImg { get; set; } = "";
    public int UserId { get; set; } = 0;
    public int CuisineId { get; set; } = 0;
    public int? OriginalRecipeId { get; set; } = null;
    }

    public class SeedRecipeData(IConfiguration configuration)
    {
        public async Task<IEnumerable<MyRecipeObject>> InsertIntoRecipe(string connectionStringName)
        {
            await using var connection = new NpgsqlConnection(configuration.GetConnectionString(connectionStringName));

            var recipesObject = new RecipesData();
            var recipeArr = recipesObject.GetRecipes();

            var recipe1Ingredients = new[] { 1, 2, 3 };
            var recipe2Ingredients = new[] { 4, 5, 6 };
            var recipe3Ingredients = new[] { 7, 8, 9 };

            List<MyRecipeObject> insertedRecipes = new List<MyRecipeObject>();

            Console.WriteLine("Inserting Recipes");
            Console.WriteLine("------------------------");

            foreach (var recipe in recipeArr)
            {
                try
                {
                    // , \"ForkedFromId\", \"OriginalRecipeId\"
                    // , '{recipe.ForkedFromId}', '{recipe.OriginalRecipeId}'
                    string query = $"INSERT INTO \"Recipe\" " +
                                    "(\"RecipeTitle\", \"TagLine\", \"Difficulty\", \"TimeToPrepare\", \"RecipeMethod\", \"Cuisine\", \"RecipeImg\", \"ForkedFromId\", \"OriginalRecipeId\", \"UserId\", \"CuisineId\") " +
                                    "VALUES (@RecipeTitle, @TagLine, @Difficulty, @TimeToPrepare, @RecipeMethod, @Cuisine, @RecipeImg, @ForkedFromId, @OriginalRecipeId, @UserId, @CuisineId) " +
                                    "RETURNING *;";
                    var insertedRecipe = await connection.QueryFirstOrDefaultAsync<MyRecipeObject>(query, recipe);
                    if (insertedRecipe != null) insertedRecipes.Add(insertedRecipe);

                    foreach (var ingredient in recipe1Ingredients)
                    {
                        string ingredientQuery = $"INSERT INTO \"RecipeIngredient\" " +
                                        "(\"Quantity\", \"RecipeId\", \"IngredientId\") " +
                                        $"VALUES ('1', '{recipe.RecipeId}', '{ingredient}') " +
                                        "RETURNING *;";

                        var insertedRecipeIngredient = await connection.QueryFirstOrDefaultAsync<MyRecipeObject>(ingredientQuery);
                        if (insertedRecipeIngredient != null) insertedRecipes.Add(insertedRecipeIngredient);
                    }
                }
                catch (Exception ex)
                {
                     Console.WriteLine($"Error inserting recipe: {ex}");
                    throw; // Re-throw the caught exception            }
                }
            }
            Console.WriteLine("------------------------");
            Console.WriteLine("Successfully inserted Recipes");
            return insertedRecipes;
        }
    }
}
