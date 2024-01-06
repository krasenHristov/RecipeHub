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
            var recipeArr = new[]
            {
                new MyRecipeObject
                {
                    RecipeId = 1,
                    RecipeTitle = "How to make pizza",
                    TagLine = "Delicious pizza",
                    Difficulty = 2,
                    TimeToPrepare = 30,
                    RecipeMethod = "1.Cook the pizza and that is it.%" +
                                   "2. Eat the pizza.%" +
                                   "3. Enjoy the pizza.%" +
                                   "4. Make more pizza.%" +
                                   "5. Eat more pizza.",
                    UserId = 1,
                    CuisineId = 1,
                    RecipeImg = "https://upload.wikimedia.org/wikipedia/commons/9/91/Pizza-3007395.jpg",
                },
                new MyRecipeObject
                {
                    RecipeId = 2,
                    RecipeTitle = "Spaghetti Bolognese",
                    TagLine = "Hearty Italian Pasta Dish",
                    Difficulty = 3,
                    TimeToPrepare = 60,
                    RecipeMethod = "1. Heat olive oil in a pan and sauté minced garlic and onions until fragrant.%" +
                                "2. Add ground beef and cook until browned. Drain excess fat.%" +
                                "3. Stir in tomato paste, crushed tomatoes, Italian herbs, and a pinch of sugar.%" +
                                "4. Simmer for 30-40 minutes to let the flavors meld.%" +
                                "5. Cook spaghetti according to package instructions.%" +
                                "6. Serve the Bolognese sauce over the cooked spaghetti. Garnish with grated Parmesan and fresh basil.",
                    UserId = 1,
                    CuisineId = 1,
                    RecipeImg = "https://images.unsplash.com/photo-1626844131082-256783844137?q=80&w=1935&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                },
                new MyRecipeObject
                {
                    RecipeId = 3,
                    RecipeTitle = "Sushi Rolls",
                    TagLine = "Fresh and Flavorful Japanese Sushi",
                    Difficulty = 4,
                    TimeToPrepare = 75,
                    RecipeMethod = "1. Prepare sushi rice according to package instructions.%" +
                                    "2. Lay a sheet of nori on a bamboo sushi rolling mat.%" +
                                    "3. Spread a thin layer of sushi rice on the nori, leaving a small border at the top.%" +
                                    "4. Add your favorite sushi ingredients (e.g., sliced fish, avocado, cucumber) along the bottom edge of the rice.%" +
                                    "5. Roll the sushi tightly using the bamboo mat.%" +
                                    "6. Slice the roll into bite-sized pieces.%" +
                                    "7. Serve with soy sauce, pickled ginger, and wasabi.",
                    UserId = 1,
                    CuisineId = 1,
                    RecipeImg = "https://images.unsplash.com/photo-1593614201641-6f16f8e41a4e?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                }
            };

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
                    string query = $"INSERT INTO \"Recipe\" " +
                                    "(\"RecipeTitle\", \"TagLine\", \"Difficulty\", \"TimeToPrepare\", \"RecipeMethod\", \"Cuisine\", \"RecipeImg\", \"UserId\", \"CuisineId\") " +
                                    $"VALUES ('{recipe.RecipeTitle}', '{recipe.TagLine}', '{recipe.Difficulty}','{recipe.TimeToPrepare}', '{recipe.RecipeMethod}', '{recipe.Cuisine}', '{recipe.RecipeImg}', '{recipe.UserId}', '{recipe.CuisineId}') " +
                                    "RETURNING *;";
                    var insertedRecipe = await connection.QueryFirstOrDefaultAsync<MyRecipeObject>(query);
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
