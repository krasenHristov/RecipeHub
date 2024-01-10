namespace OpenSourceRecipes.Seeds;

public class RecipesData
{
    public MyRecipeObject[] GetRecipes()
    {
        return new[]
            {

                new MyRecipeObject
                {
                    RecipeId = 21,
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
                    RecipeImg = "https://cookieandkate.com/images/2021/07/margherita-pizza-recipe-1-2-768x1155.jpg", // done
                },

                new MyRecipeObject
                {
                    RecipeId = 22,
                    RecipeTitle = "Caprese Pizza",
                    TagLine = "Italian-Inspired Caprese Pizza with a Twist",
                    Difficulty = 3,
                    TimeToPrepare = 90,
                    Cuisine = "Italian",
                    RecipeMethod = "1. Prepare pizza dough and let it rise.%" +
                                   "2. Spread a thin layer of basil pesto on the dough.%" +
                                   "3. Arrange slices of fresh mozzarella cheese, ripe cherry tomatoes, and fresh basil leaves on top.%" +
                                   "4. Drizzle with balsamic glaze and a touch of olive oil.%" +
                                   "5. Bake in a preheated oven at 475°F for about 10-12 minutes.%" +
                                   "6. Serve hot, garnished with extra basil and a sprinkle of sea salt.",
                    UserId = 2,
                    ForkedFromId = 1,
                    OriginalRecipeId = 1,
                    CuisineId = 1,
                    RecipeImg = "https://www.dishbydish.net/wp-content/uploads/Gluten-Free-Caprese-Pizza-Dairy-Free_Final3-scaled.jpg" // done
                },

                new MyRecipeObject
                {
                    RecipeId = 23,
                    RecipeTitle = "Caprese Pizza",
                    TagLine = "Italian-Inspired Caprese Pizza with a Twist",
                    Difficulty = 3,
                    TimeToPrepare = 90,
                    Cuisine = "Italian",
                    RecipeMethod = "1. Prepare pizza dough and let it rise.%" +
                                   "2. Spread a thin layer of basil pesto on the dough.%" +
                                   "3. Arrange slices of fresh mozzarella cheese, ripe cherry tomatoes, and fresh basil leaves on top.%" +
                                   "4. Drizzle with balsamic glaze and a touch of olive oil.%" +
                                   "5. Bake in a preheated oven at 475°F for about 10-12 minutes.%" +
                                   "6. Serve hot, garnished with extra basil and a sprinkle of sea salt.",
                    UserId = 2,
                    ForkedFromId = 1,
                    OriginalRecipeId = 1,
                    CuisineId = 1,
                    RecipeImg = "https://www.theendlessmeal.com/wp-content/uploads/2022/06/caprese-pizza-6-730x1094.jpg" // done
                },

                new MyRecipeObject
                {
                    RecipeId = 24,
                    RecipeTitle = "Caprecozza Pizza",
                    TagLine = "A Delicious Twist with Ham on the Classic Capricozza Pizza",
                    Difficulty = 3,
                    TimeToPrepare = 90,
                    Cuisine = "Italian",
                    RecipeMethod = "1. Prepare pizza dough and let it rise.%" +
                                   "2. Spread a thin layer of basil pesto on the dough.%" +
                                   "3. Arrange slices of fresh mozzarella cheese, ripe cherry tomatoes, fresh basil leaves, and ham slices on top.%" +
                                   "4. Drizzle with balsamic glaze and a touch of olive oil.%" +
                                   "5. Bake in a preheated oven at 475°F for about 10-12 minutes.%" +
                                   "6. Serve hot, garnished with extra basil and a sprinkle of sea salt.",
                    UserId = 2,
                    ForkedFromId = 2,
                    OriginalRecipeId = 1,
                    CuisineId = 1,
                    RecipeImg = "https://www.italianstylecooking.net/wp-content/uploads/2022/01/Pizza-capricciosa-768x512.jpg" // done
                },

                new MyRecipeObject
                {
                    RecipeId = 25,
                    RecipeTitle = "Hawaiian Luau Pizza",
                    TagLine = "A Tropical Delight with Ham and Pineapple",
                    Difficulty = 3,
                    TimeToPrepare = 90,
                    Cuisine = "Italian",
                    RecipeMethod = "1. Prepare pizza dough and let it rise.%" +
                                   "2. Spread a thin layer of tomato sauce on the dough.%" +
                                   "3. Arrange slices of fresh mozzarella cheese, ham slices, and pineapple chunks on top.%" +
                                   "4. Drizzle with a touch of olive oil.%" +
                                   "5. Bake in a preheated oven at 475°F for about 10-12 minutes.%" +
                                   "6. Serve hot, bringing the taste of the Hawaiian luau to your pizza night!",
                    UserId = 2,
                    ForkedFromId = 4,
                    OriginalRecipeId = 1,
                    CuisineId = 1,
                    RecipeImg = "https://sallysbakingaddiction.com/wp-content/uploads/2014/08/It-doesnt-get-much-better-than-Homemade-Hawaiian-Pizza.-Tropical-paradise-for-dinner-2.jpg" // done
                },

                new MyRecipeObject
                {
                    RecipeId = 26,
                    RecipeTitle = "Caprese Pesto Chicken Pizza",
                    TagLine = "A Delicious Fusion of Caprese and Pesto Chicken on Pizza",
                    Difficulty = 3,
                    TimeToPrepare = 30,
                    Cuisine = "Italian",
                    RecipeMethod = "1. Prepare pizza dough and let it rise.%" +
                                   "2. Spread a thin layer of basil pesto on the dough as the pizza sauce.%" +
                                   "3. Season boneless, skinless chicken breast with salt and pepper. Cook it in a pan until fully cooked, then slice it into thin strips.%" +
                                   "4. Arrange slices of fresh mozzarella cheese, halved cherry tomatoes, and the cooked chicken strips on top of the pesto.%" +
                                   "5. Drizzle balsamic glaze and a touch of olive oil over the ingredients.%" +
                                   "6. Bake in a preheated oven at 475°F for about 10-12 minutes or until the crust is golden and the cheese is bubbly.%" +
                                   "7. Garnish with fresh basil leaves.%" +
                                   "8. Serve hot and enjoy!",
                    UserId = 2,
                    ForkedFromId = 2,
                    OriginalRecipeId = 1,
                    CuisineId = 1,
                    RecipeImg = "https://popmenucloud.com/cdn-cgi/image/width%3D1920%2Cheight%3D1920%2Cfit%3Dscale-down%2Cformat%3Dauto%2Cquality%3D60/jchyqlfk/5e21852f-acda-4b9d-a36d-0ec7c7414a63.jpg" // done
                },

                new MyRecipeObject
                {
                    RecipeId = 31,
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
                    RecipeImg = "https://images.unsplash.com/photo-1626844131082-256783844137?q=80&w=1935&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", // done
                },

                new MyRecipeObject
                {
                    RecipeId = 32,
                    RecipeTitle = "Creamy Mushroom Bolognese Pasta",
                    TagLine = "A Fusion of Creamy and Hearty Italian Flavors",
                    Difficulty = 3,
                    TimeToPrepare = 45,
                    Cuisine = "Italian",
                    RecipeMethod = "1. In a large skillet, heat olive oil and sauté minced garlic and onions until fragrant.%" +
                                   "2. Add ground beef and cook until browned. Drain excess fat.%" +
                                   "3. Stir in tomato paste, crushed tomatoes, Italian herbs, and a pinch of sugar. Simmer for 20-25 minutes to let the flavors meld.%" +
                                   "4. In another skillet, melt butter over medium heat. Add sliced mushrooms and sauté until they release their moisture and become tender. Remove from the skillet and set aside.%" +
                                   "5. In the same skillet, add minced garlic and cook until fragrant.%" +
                                   "6. Pour in heavy cream, grated Parmesan cheese, and cream cheese. Stir until the cheeses are fully melted and the sauce is creamy.%" +
                                   "7. Return the cooked mushrooms to the skillet with the creamy Alfredo sauce and stir to combine.%" +
                                   "8. Season with salt, pepper, and a pinch of nutmeg for flavor.%" +
                                   "9. Cook your favorite pasta according to package instructions, then drain.%" +
                                   "10. Toss the cooked pasta in the creamy mushroom Bolognese sauce.%" +
                                   "11. Serve hot, garnished with fresh parsley and additional grated Parmesan cheese.",
                    UserId = 1,
                    ForkedFromId = 7,
                    OriginalRecipeId = 7,
                    CuisineId = 1,
                    RecipeImg = "https://www.foodandwine.com/thmb/fn-_T5155EZTdPsiY0FQw8qLZj0=/750x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/201304-xl-mushroom-bolognese-2000-8c3e9990d0394b4cba68643a9e533921.jpg" // done
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
                    RecipeId = 7,
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
                    RecipeImg = "https://images.unsplash.com/photo-1551504734-5ee1c4a1479b?q=80&w=1470&auto=format&fit=[…]3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                },

                new MyRecipeObject
                {
                    RecipeId = 8,
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
                    RecipeImg = "https://images.unsplash.com/photo-1598514983318-2f64f8f4796c?q=80&w=1470&auto=format&f[…]3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                },

                new MyRecipeObject
                {
                    RecipeId = 9,
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
                    RecipeId = 10,
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
                    RecipeId = 11,
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
                },

                new MyRecipeObject
                {
                RecipeId = 12,
                RecipeTitle = "Spaghetti Carbonara",
                TagLine = "Creamy Italian Pasta",
                Difficulty = 3,
                TimeToPrepare = 25,
                Cuisine = "Italian",
                RecipeMethod = "1. Cook spaghetti according to package instructions.%" +
                               "2. In a separate pan, fry pancetta until crispy.%" +
                               "3. Whisk together eggs, grated Pecorino Romano cheese, and black pepper in a bowl.%" +
                               "4. Drain cooked spaghetti and combine with the pancetta.%" +
                               "5. Pour the egg and cheese mixture over the spaghetti and toss until creamy.%" +
                               "6. Serve hot, garnished with more cheese and black pepper.",
                UserId = 1,
                CuisineId = 1,
                RecipeImg = "https://www.chompslurrpburp.com/wp-content/uploads/2021/12/spaghetti-carbonara-800x500.jpg"
                },

                new MyRecipeObject
                {
                    RecipeId = 13,
                    RecipeTitle = "Vegetarian Chili",
                    TagLine = "Hearty and Healthy",
                    Difficulty = 2,
                    TimeToPrepare = 40,
                    Cuisine = "Mexican",
                    RecipeMethod = "1. Sauté onions, garlic, and bell peppers in a large pot.%" +
                                   "2. Add chili powder, cumin, and paprika; stir briefly.%" +
                                   "3. Add beans, tomatoes, corn, and vegetable broth.%" +
                                   "4. Simmer until chili thickens and flavors meld.%" +
                                   "5. Serve hot with toppings like cheese, sour cream, and cilantro.",
                    UserId = 3,
                    CuisineId = 3,
                    RecipeImg = "https://images.immediate.co.uk/production/volatile/sites/30/2022/10/Vegetarian-chilli-206c469.jpg?quality=90&webp=true&resize=300,272"
                },

                new MyRecipeObject
                {
                    RecipeId = 14,
                    RecipeTitle = "Vegetable Fried Rice",
                    TagLine = "Quick and Flavorful",
                    Difficulty = 2,
                    TimeToPrepare = 20,
                    Cuisine = "Japanese",
                    RecipeMethod = "1. Cook rice and let it cool.%" +
                                   "2. Heat oil in a wok or skillet, and stir-fry mixed vegetables.%" +
                                   "3. Add cooked rice and soy sauce, tossing to combine.%" +
                                   "4. Push rice to the side and scramble eggs in the pan.%" +
                                   "5. Mix eggs into the rice and serve hot with green onions.",
                    UserId = 5,
                    CuisineId = 3,
                    RecipeImg = "https://shwetainthekitchen.com/wp-content/uploads/2023/06/veg-fried-rice.jpg"
                },

                new MyRecipeObject
                {
                    RecipeId = 15,
                    RecipeTitle = "Guacamole",
                    TagLine = "Fresh and Flavorful Dip",
                    Difficulty = 1,
                    TimeToPrepare = 15,
                    Cuisine = "Mexican",
                    RecipeMethod = "1. Mash ripe avocados in a bowl.%" +
                                   "2. Add finely chopped onion, tomato, cilantro, and jalapeño.%" +
                                   "3. Squeeze in lime juice and season with salt and pepper.%" +
                                   "4. Mix well and serve with tortilla chips or as a topping.",
                    UserId = 6,
                    CuisineId = 2,
                    RecipeImg = "https://www.gimmesomeoven.com/wp-content/uploads/2012/08/The-Best-Guacamole-Recipe-4.jpg"
                }
            };
    }

    public int[][] ingredients()
    {
        return new int[][]
        {
            new int[] {217, 153, 31, 347, 242, 378}, //21
            new int[] {217, 137, 31, 633, 347, 108, 242, 378}, // 22
            new int[] {217, 153, 31, 633 , 347, 108, 242, 378}, // 23
            new int[] {217, 137, 31, 633, 347, 544, 108, 242, 378}, // 24
            new int[] {217, 153, 31, 544, 479, 242}, // 25
            new int[] {217, 137, 528, 31, 633, 108, 242, 378, 349, 347}, // 26
            new int[] { 242, 363, 368, 543, 422, 396, 663, 249, 692, 34, 347 }, // 31
            new int[] { 682, 242, 638, 543, 153, 396, 663, 249, 5, 634, 363, 27, 34, 16, 371, 378, 349 }, // 32
            new int[] {10, 11, 12, 13, 14, 15, 16, 17, 18},
            new int[] {19, 20, 21, 22, 23, 24, 25, 26, 27},
            new int[] {28, 29, 30, 31, 32, 33, 34, 35, 36},
            new int[] {1, 2, 3, 40, 41, 42, 43, 44, 45},
            new int[] {19, 20, 21, 22, 23, 24, 25, 26, 27},
            new int[] {28, 29, 30, 31, 32, 33, 34, 35, 36},
            new int[] {37, 38, 39, 40, 41, 42, 43, 44, 45},
            new int[] {1, 2, 3, 22, 23, 24, 25, 26, 27},
            new int[] {28, 29, 30, 31, 32, 33, 34, 35, 36},
            new int[] {37, 38, 39, 40, 41, 42, 43, 44, 45},
            new int[] {37, 38, 39, 40, 41, 42, 43, 44, 45},
            new int[] {1, 2, 3, 22, 23, 24, 25, 26, 27},
            new int[] {28, 29, 30, 31, 32, 33, 34, 35, 36},
        };
    }

    public string[][] quantities()
    {
        return new string[][]
        {
            new string[] { "300g", "1/3 cup", "170g", "25g", "2 tbs", "To taste" }, // 21
            new string[] { "300g", "3 tbs", "170g", "6-8 pcs", "6-8 leaves", "1-2 tbs", "1-2 tbs", "To taste" }, // 22
            new string[] { "300g", "1/4 cup", "115g", "6-8 pcs", "8-10 leaves", "1 tbs", "2 tbs", "To taste"}, // 23
            new string[] { "300g", "3 tbs", "170g", "8 pcs", "10 leaves", "4 slices", "2 tbs", "2 tbs", "To taste"}, // 24
            new string[] { "250g", "1/4 cup", "130g", "3 slices", "1/2 cup of chunks", "2 tbs"}, // 25
            new string[] { "300g", "2-3 tbs", "150g", "150g", "4-6 pcs", "1-2 tbs", "1-2 tbs", "To taste", "To taste", "2-4 leaves"}, //26
            new string[] { "2 tbs", "2 cloves", "1 med size", "500g", "2 tbs", "400g", "2 tsp", "1/2 tsp", "300g", "For garnish", "For garnish" }, // 31
            new string[] { "300g", "2 cloves", "1 med size", "500g", "2 tbs", "400g", "2 tbs", "1/2 tbs", "2 tbs", "250g", "2 cloves", "1 cup", "1/2 cup", "1/4 cup", "1/4 tbs", "To taste", "To taste" }, // 32
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
            new string[] { "37g", "391g", "402g", "413g", "424g", "435g", "446g", "457g", "468g" },
            new string[] { "19g", "201g", "212g", "223g", "234g", "245g", "256g", "267g", "278g" },
            new string[] { "28g", "291g", "302g", "313g", "324g", "335g", "346g", "357g", "368g" },
        };
    }
}
