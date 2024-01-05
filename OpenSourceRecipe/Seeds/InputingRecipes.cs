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
    public string Cuisine { get; set; } = "";
    public string RecipeImg { get; set; } = "";
    public int UserId { get; set; } = 0;
    public int CuisineId { get; set; } = 0;
    }

    public class SeedRecipeData(IConfiguration configuration)
    {
        public async Task<IEnumerable<MyRecipeObject>> InsertIntoRecipe()
        {
            await using var connection = new NpgsqlConnection(configuration.GetConnectionString("TestConnection"));
            var recipeArr = new[]
            {
                new MyRecipeObject
                {
                    RecipeId = 0,
                    RecipeTitle = "How to make pizza",
                    TagLine = "Delicious pizza",
                    Difficulty = 2,
                    TimeToPrepare = 30,
                    RecipeMethod = "1.Cook the fucking pizza and thats it.",
                    PostedOn = DateTime.Now.ToString("yyyy-MM-dd"),
                    UserId = 0,
                    CuisineId = 0,
                    RecipeImg = "https://upload.wikimedia.org/wikipedia/commons/9/91/Pizza-3007395.jpg"
                },
                new MyRecipeObject
                {
                    RecipeId = 1,
                    RecipeTitle = "Spaghetti Bolognese",
                    TagLine = "Hearty Italian Pasta Dish",
                    Difficulty = 3,
                    TimeToPrepare = 60,
                    RecipeMethod = "1. Heat olive oil in a pan and sauté minced garlic and onions until fragrant.\n" +
                                "2. Add ground beef and cook until browned. Drain excess fat.\n" +
                                "3. Stir in tomato paste, crushed tomatoes, Italian herbs, and a pinch of sugar.\n" +
                                "4. Simmer for 30-40 minutes to let the flavors meld.\n" +
                                "5. Cook spaghetti according to package instructions.\n" +
                                "6. Serve the Bolognese sauce over the cooked spaghetti. Garnish with grated Parmesan and fresh basil.",
                    PostedOn = DateTime.Now.ToString("yyyy-MM-dd"),
                    UserId = 0,
                    CuisineId = 0,
                    RecipeImg = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0d/Bolognese_sauce_over_pasta.jpg/800px-Bolognese_sauce_over_pasta.jpg",
                },
                new MyRecipeObject
                {
                    RecipeId = 2,
                    RecipeTitle = "Sushi Rolls",
                    TagLine = "Fresh and Flavorful Japanese Sushi",
                    Difficulty = 4,
                    TimeToPrepare = 75,
                    RecipeMethod = "1. Prepare sushi rice according to package instructions.\n" +
                                    "2. Lay a sheet of nori on a bamboo sushi rolling mat.\n" +
                                    "3. Spread a thin layer of sushi rice on the nori, leaving a small border at the top.\n" +
                                    "4. Add your favorite sushi ingredients (e.g., sliced fish, avocado, cucumber) along the bottom edge of the rice.\n" +
                                    "5. Roll the sushi tightly using the bamboo mat.\n" +
                                    "6. Slice the roll into bite-sized pieces.\n" +
                                    "7. Serve with soy sauce, pickled ginger, and wasabi.",
                    PostedOn = DateTime.Now.ToString("yyyy-MM-dd"),
                    UserId = 0,
                    CuisineId = 0,
                    RecipeImg = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/6b/Sushi_Platter.jpg/800px-Sushi_Platter.jpg",
                }
            };

            List<MyRecipeObject> insertedRecipes = new List<MyRecipeObject>();

            Console.WriteLine("Inserting Recipes");
            Console.WriteLine("------------------------");

            foreach (var recipe in recipeArr)
            {
                string query = $"INSERT INTO \"Recipe\" " +
                                "(\"RecipeId\", \"RecipeTitle\", \"TagLine\", \"Difficulty\", \"TimeToPrepare\", \"RecipeMethod\", \"PostedOn\", \"Cuisine\", \"RecipeImg\", \"UserId\", \"CuisineId\") " +
                                $"VALUES ('{recipe.RecipeId}', '{recipe.RecipeTitle}', '{recipe.TagLine}', '{recipe.Difficulty}','{recipe.TimeToPrepare}', '{recipe.RecipeMethod}', '{recipe.PostedOn}', '{recipe.Cuisine}', '{recipe.RecipeImg}', '{recipe.UserId}', '{recipe.CuisineId}') " +
                                "RETURNING *;";
                var insertedRecipe = await connection.QueryFirstOrDefaultAsync<MyRecipeObject>(query);
                if (insertedRecipe != null) insertedRecipes.Add(insertedRecipe);
            }

            Console.WriteLine("------------------------");
            Console.WriteLine("Successfully inserted Recipes");
            return insertedRecipes;
        }
    }
}