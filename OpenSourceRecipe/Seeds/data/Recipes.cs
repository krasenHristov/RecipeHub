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
                },

                new MyRecipeObject
                {
                    RecipeId = 4,
                    RecipeTitle = "Margherita Pizza",
                    TagLine = "Classic Italian Pizza with Fresh Basil",
                    Difficulty = 3,
                    TimeToPrepare = 90,
                    Cuisine = "Italian",
                    RecipeMethod = "1. Prepare pizza dough and let it rise.%" +
                                    "2. Spread a thin layer of tomato sauce on the dough.%" +
                                    "3. Top with slices of fresh mozzarella cheese and fresh basil leaves.%" +
                                    "4. Drizzle with olive oil and season with salt.%" +
                                    "5. Bake in a preheated oven at 475°F for about 10-12 minutes.%" +
                                    "6. Serve hot, garnished with more fresh basil.",
                    UserId = 2,
                    CuisineId = 1,
                    RecipeImg = "https://images.unsplash.com/photo-1542282811-943ef1a977c3?ixlib=rb-1.2.1&q=80&w=1080&auto=format&fit=crop",
                },

                new MyRecipeObject
                {
                    RecipeId = 5,
                    RecipeTitle = "Tacos al Pastor",
                    TagLine = "Delicious Mexican Tacos with Pineapple and Pork",
                    Difficulty = 4,
                    TimeToPrepare = 60,
                    Cuisine = "Mexican",
                    RecipeMethod = "1. Marinate sliced pork in a mix of chili peppers, spices, and pineapple juice.%" +
                                    "2. Grill the pork until it's cooked through.%" +
                                    "3. Warm corn tortillas on a skillet.%" +
                                    "4. Place the grilled pork on the tortillas.%" +
                                    "5. Top with diced pineapple, chopped onion, and fresh cilantro.%" +
                                    "6. Serve with a slice of lime and salsa.",
                    UserId = 3,
                    CuisineId = 2,
                    RecipeImg = "https://images.unsplash.com/photo-1592337933347-f674e6e11d1b?ixlib=rb-1.2.1&q=80&w=1080&auto=format&fit=crop",
                },

                new MyRecipeObject
                {
                    RecipeId = 6,
                    RecipeTitle = "Chicken Teriyaki",
                    TagLine = "Sweet and Savory Japanese Dish",
                    Difficulty = 2,
                    TimeToPrepare = 30,
                    Cuisine = "Japanese",
                    RecipeMethod = "1. Cut chicken into bite-sized pieces and season.%" +
                                    "2. Cook the chicken in a skillet until browned.%" +
                                    "3. Add teriyaki sauce to the skillet and simmer.%" +
                                    "4. Cook until the sauce thickens and coats the chicken.%" +
                                    "5. Serve over steamed rice, garnished with sesame seeds and sliced green onions.",
                    UserId = 4,
                    CuisineId = 3,
                    RecipeImg = "https://images.unsplash.com/photo-1576866209830-cf912e83c04b?ixlib=rb-1.2.1&q=80&w=1080&auto=format&fit=crop",
                },

                new MyRecipeObject
                {
                    RecipeId = 7,
                    RecipeTitle = "Grilled Margherita Pizza",
                    TagLine = "Smoky and Charred Twist on Classic Italian Pizza",
                    Difficulty = 3,
                    TimeToPrepare = 70,
                    Cuisine = "Italian",
                    RecipeMethod = "1. Prepare pizza dough and let it rise.%" +
                                   "2. Preheat grill to high heat.%" +
                                   "3. Roll out the dough and lightly oil one side.%" +
                                   "4. Place dough oil-side down on the grill. Grill for 2 minutes.%" +
                                   "5. Flip the dough, spread tomato sauce, add mozzarella and basil.%" +
                                   "6. Close grill lid and cook for 3-4 minutes until cheese melts.%" +
                                   "7. Serve hot, garnished with fresh basil and a drizzle of olive oil.",
                    UserId = 2,
                    CuisineId = 1,
                    RecipeImg = "https://images.unsplash.com/photo-1572183048591-67d4c43b0314?ixlib=rb-1.2.1&q=80&w=1080&auto=format&fit=crop",
                    ForkedFromId = 4,
                    OriginalRecipeId = 4
                },

                new MyRecipeObject
                {
                    RecipeId = 8,
                    RecipeTitle = "Oven-Baked Tacos al Pastor",
                    TagLine = "Easy Home Version of Traditional Mexican Tacos",
                    Difficulty = 3,
                    TimeToPrepare = 90,
                    Cuisine = "Mexican",
                    RecipeMethod = "1. Marinate pork in chili, spices, and pineapple juice overnight.%" +
                                   "2. Preheat oven to 350°F (175°C).%" +
                                   "3. Place marinated pork in a baking dish and cover with foil.%" +
                                   "4. Bake for 60 minutes or until pork is tender.%" +
                                   "5. Shred the pork and serve on warmed tortillas with toppings.%" +
                                   "6. Garnish with pineapple, onion, cilantro, and lime.",
                    UserId = 3,
                    CuisineId = 2,
                    RecipeImg = "https://images.unsplash.com/photo-1604908554025-f09d8cc4c5d5?ixlib=rb-1.2.1&q=80&w=1080&auto=format&fit=crop",
                    ForkedFromId = 5,
                    OriginalRecipeId = 5
                },

                new MyRecipeObject
                {
                    RecipeId = 9,
                    RecipeTitle = "Baked Chicken Teriyaki",
                    TagLine = "Oven-Baked for an Easy Twist on the Classic",
                    Difficulty = 2,
                    TimeToPrepare = 45,
                    Cuisine = "Japanese",
                    RecipeMethod = "1. Cut chicken into pieces and place in a baking dish.%" +
                                   "2. Mix teriyaki sauce with garlic, ginger, and pour over chicken.%" +
                                   "3. Let marinate for 30 minutes.%" +
                                   "4. Preheat oven to 375°F (190°C).%" +
                                   "5. Bake chicken for 25-30 minutes, basting occasionally.%" +
                                   "6. Serve over rice, garnished with sesame seeds and green onions.",
                    UserId = 4,
                    CuisineId = 3,
                    RecipeImg = "https://images.unsplash.com/photo-1615486767794-4b74a4b1c1e1?ixlib=rb-1.2.1&q=80&w=1080&auto=format&fit=crop",
                    ForkedFromId = 6,
                    OriginalRecipeId = 6
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
            new int[] {19, 20, 21, 22, 23, 24, 25, 26, 27},
            new int[] {28, 29, 30, 31, 32, 33, 34, 35, 36},
            new int[] {37, 38, 39, 40, 41, 42, 43, 44, 45},
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
            new string[] { "19g", "201g", "212g", "223g", "234g", "245g", "256g", "267g", "278g" },
            new string[] { "28g", "291g", "302g", "313g", "324g", "335g", "346g", "357g", "368g" },
            new string[] { "37g", "391g", "402g", "413g", "424g", "435g", "446g", "457g", "468g" },
            new string[] { "19g", "201g", "212g", "223g", "234g", "245g", "256g", "267g", "278g" },
            new string[] { "28g", "291g", "302g", "313g", "324g", "335g", "346g", "357g", "368g" },
            new string[] { "37g", "391g", "402g", "413g", "424g", "435g", "446g", "457g", "468g" },
        };
    }
}
