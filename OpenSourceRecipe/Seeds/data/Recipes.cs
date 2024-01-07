namespace OpenSourceRecipes.Seeds;

public class RecipesData
{
    public MyRecipeObject[] GetRecipes()
    {
        return new[]
            {

                new MyRecipeObject
                {
                    RecipeId = 1,
                    RecipeTitle = "How to make pizza",
                    TagLine = "Delicious pizza",
                    Difficulty = 2,
                    TimeToPrepare = 30,
                    Cuisine = "Italian",
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
                    Cuisine = "Italian",
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
                    Cuisine = "Japanese",
                    RecipeMethod = "1. Prepare sushi rice according to package instructions.%" +
                                    "2. Lay a sheet of nori on a bamboo sushi rolling mat.%" +
                                    "3. Spread a thin layer of sushi rice on the nori, leaving a small border at the top.%" +
                                    "4. Add your favorite sushi ingredients (e.g., sliced fish, avocado, cucumber) along the bottom edge of the rice.%" +
                                    "5. Roll the sushi tightly using the bamboo mat.%" +
                                    "6. Slice the roll into bite-sized pieces.%" +
                                    "7. Serve with soy sauce, pickled ginger, and wasabi.",
                    UserId = 1,
                    CuisineId = 3,
                    RecipeImg = "https://images.unsplash.com/photo-1593614201641-6f16f8e41a4e?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                },

                new MyRecipeObject
                {
                    RecipeId = 4,
                    RecipeTitle = "Sushi Rolls",
                    TagLine = "Fresh and Flavorful Japanese Sushi",
                    Difficulty = 4,
                    TimeToPrepare = 75,
                    Cuisine = "Japanese",
                    RecipeMethod = "1. Prepare sushi rice according to package instructions.%" +
                                    "2. Lay a sheet of nori on a bamboo sushi rolling mat.%" +
                                    "3. Spread a thin layer of sushi rice on the nori, leaving a small border at the top.%" +
                                    "4. Add your favorite sushi ingredients (e.g., sliced fish, avocado, cucumber) along the bottom edge of the rice.%" +
                                    "5. Roll the sushi tightly using the bamboo mat.%" +
                                    "6. Slice the roll into bite-sized pieces.%" +
                                    "7. Serve with soy sauce",
                    UserId = 1,
                    CuisineId = 3,
                    RecipeImg = "https://images.unsplash.com/photo-1593614201641-6f16f8e41a4e?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    ForkedFromId = 3,
                    OriginalRecipeId = 3
                },

                new MyRecipeObject
                {
                    RecipeId = 5,
                    RecipeTitle = "Sushi Rolls",
                    TagLine = "Fresh and Flavorful Japanese Sushi",
                    Difficulty = 4,
                    TimeToPrepare = 75,
                    Cuisine = "Japanese",
                    RecipeMethod = "1. Prepare sushi rice according to package instructions.%" +
                                    "2. Lay a sheet of nori on a bamboo sushi rolling mat.%" +
                                    "3. Spread a thin layer of sushi rice on the nori, leaving a small border at the top.%" +
                                    "4. Add your favorite sushi ingredients (e.g., sliced fish, avocado, cucumber) along the bottom edge of the rice.%" +
                                    "5. Roll the sushi tightly using the bamboo mat.%" +
                                    "6. Slice the roll into bite-sized pieces.%" +
                                    "7. Serve with soy sauce, pickled ginger",
                    UserId = 1,
                    CuisineId = 3,
                    RecipeImg = "https://images.unsplash.com/photo-1593614201641-6f16f8e41a4e?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    ForkedFromId = 4,
                    OriginalRecipeId = 3
                }
            };
    }

    public int[][] ingredients()
    {
        return new int[][]
        {
            new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9},
            new int[] {10, 11, 12, 13, 14, 15, 16, 17, 18},
            new int[] {19, 20, 21, 22, 23, 24, 25, 26, 27},
            new int[] {28, 29, 30, 31, 32, 33, 34, 35, 36},
            new int[] {37, 38, 39, 40, 41, 42, 43, 44, 45},
        };
    }

    public string[][] quantities()
    {
        return new string[][]
        {
            new string[] { "100g", "225g", "34g", "4", "52g", "6kg", "712g", "811g", "91g" },
            new string[] { "10g", "112g", "123g", "134g", "145g", "156g", "167g", "178g", "189g" },
            new string[] { "19g", "201g", "212g", "223g", "234g", "245g", "256g", "267g", "278g" },
            new string[] { "28g", "291g", "302g", "313g", "324g", "335g", "346g", "357g", "368g" },
            new string[] { "37g", "391g", "402g", "413g", "424g", "435g", "446g", "457g", "468g" },
        };
    }
}
