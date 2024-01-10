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
                    RecipeId = 33,
                    RecipeTitle = "Spicy Sausage and Mushroom Linguine",
                    TagLine = "A Flavorful Twist on Classic Italian Pasta",
                    Difficulty = 3,
                    TimeToPrepare = 45,
                    Cuisine = "Italian",
                    RecipeMethod = "1. Heat olive oil in a pan and sauté minced garlic and sliced mushrooms until fragrant and mushrooms are tender.%" +
                                   "2. Remove the mushroom mixture from the pan and set aside.%" +
                                   "3. In the same pan, add spicy Italian sausage, breaking it into crumbles, and cook until browned and cooked through. Drain excess fat.%" +
                                   "4. Return the mushroom mixture to the pan with the sausage.%" +
                                   "5. Stir in crushed tomatoes, red pepper flakes (for a spicy kick), and Italian herbs. Simmer for 15-20 minutes to let the flavors meld.%" +
                                   "6. Cook linguine according to package instructions.%" +
                                   "7. Toss the cooked linguine in the spicy sausage and mushroom sauce.%" +
                                   "8. Serve hot, garnished with fresh parsley and grated Parmesan cheese.",
                    UserId = 1,
                    ForkedFromId = 7,
                    OriginalRecipeId = 7,
                    CuisineId = 1,
                    RecipeImg = "https://www.chilipeppermadness.com/wp-content/uploads/2021/07/Bucatini-all-Amatriciana-Recipe1.jpg" // done
                },

                new MyRecipeObject
                {
                    RecipeId = 34,
                    RecipeTitle = "Bolognese Stuffed Bell Peppers",
                    TagLine = "A Unique Twist on Bolognese Served in Bell Peppers",
                    Difficulty = 3,
                    TimeToPrepare = 60,
                    Cuisine = "Italian",
                    RecipeMethod = "1. Preheat the oven to 375°F (190°C).%" +
                                   "2. Cut the tops off bell peppers and remove the seeds and membranes. Set aside.%" +
                                   "3. In a skillet, heat olive oil and sauté minced garlic and onions until fragrant.%" +
                                   "4. Add ground beef and cook until browned. Drain excess fat.%" +
                                   "5. Stir in tomato paste, crushed tomatoes, Italian herbs, and a pinch of sugar. Simmer for 30-40 minutes to let the flavors meld.%" +
                                   "6. In a mixing bowl, combine the Bolognese sauce with cooked rice and grated Parmesan cheese.%" +
                                   "7. Stuff each bell pepper with the Bolognese and rice mixture.%" +
                                   "8. Place the stuffed bell peppers in a baking dish. Cover with aluminum foil.%" +
                                   "9. Bake in the preheated oven for 30-35 minutes or until the peppers are tender.%" +
                                   "10. Remove the foil, sprinkle with mozzarella cheese, and bake for an additional 5-10 minutes until the cheese is bubbly and golden.%" +
                                   "11. Serve hot, garnished with fresh basil leaves.",
                    UserId = 1,
                    ForkedFromId = 7,
                    OriginalRecipeId = 7,
                    CuisineId = 1,
                    RecipeImg = "https://itsavegworldafterall.com/wp-content/uploads/2018/09/Spaghetti-Stuffed-Bell-Peppers-4.jpg" // done
                },

                new MyRecipeObject
                {
                    RecipeId = 35,
                    RecipeTitle = "Bolognese Zucchini Boats",
                    TagLine = "A Low-Carb Twist on Bolognese Served in Zucchini Halves",
                    Difficulty = 3,
                    TimeToPrepare = 60,
                    Cuisine = "Italian",
                    RecipeMethod = "1. Preheat the oven to 375°F (190°C).%" +
                                   "2. Cut zucchinis in half lengthwise and scoop out the seeds and flesh to create 'boats.'%" +
                                   "3. In a skillet, heat olive oil and sauté minced garlic and onions until fragrant.%" +
                                   "4. Add ground beef and cook until browned. Drain excess fat.%" +
                                   "5. Stir in tomato paste, crushed tomatoes, Italian herbs, and a pinch of sugar. Simmer for 30-40 minutes to let the flavors meld.%" +
                                   "6. In a mixing bowl, combine the Bolognese sauce with cooked rice and grated Parmesan cheese.%" +
                                   "7. Fill each zucchini boat with the Bolognese and rice mixture.%" +
                                   "8. Place the filled zucchini boats in a baking dish.%" +
                                   "9. Cover with aluminum foil and bake in the preheated oven for 25-30 minutes.%" +
                                   "10. Remove the foil, sprinkle with mozzarella cheese, and bake for an additional 5-10 minutes until the cheese is bubbly and golden.%" +
                                   "11. Serve hot, garnished with fresh basil leaves.",
                    UserId = 1,
                    ForkedFromId = 7,
                    OriginalRecipeId = 7,
                    CuisineId = 1,
                    RecipeImg = "https://www.howsweeteats.com/wp-content/uploads/2022/08/zucchini-boat-bolognese-14.jpg" // done
                },

                new MyRecipeObject
                {
                    RecipeId = 36,
                    RecipeTitle = "Bolognese Stuffed Portobello Mushrooms",
                    TagLine = "A Delicious Twist on Bolognese Served in Portobello Mushroom Caps",
                    Difficulty = 3,
                    TimeToPrepare = 60,
                    Cuisine = "Italian",
                    RecipeMethod = "1. Preheat the oven to 375°F (190°C).%" +
                                   "2. Remove the stems and gills from portobello mushroom caps to create 'mushroom bowls.'%" +
                                   "3. In a skillet, heat olive oil and sauté minced garlic and onions until fragrant.%" +
                                   "4. Add ground beef and cook until browned. Drain excess fat.%" +
                                   "5. Stir in tomato paste, crushed tomatoes, Italian herbs, and a pinch of sugar. Simmer for 30-40 minutes to let the flavors meld.%" +
                                   "6. In a mixing bowl, combine the Bolognese sauce with cooked rice and grated Parmesan cheese.%" +
                                   "7. Fill each portobello mushroom cap with the Bolognese and rice mixture.%" +
                                   "8. Place the stuffed mushroom caps in a baking dish.%" +
                                   "9. Cover with aluminum foil and bake in the preheated oven for 25-30 minutes.%" +
                                   "10. Remove the foil, sprinkle with mozzarella cheese, and bake for an additional 5-10 minutes until the cheese is bubbly and golden.%" +
                                   "11. Serve hot, garnished with fresh basil leaves.",
                    UserId = 1,
                    ForkedFromId = 7,
                    OriginalRecipeId = 7,
                    CuisineId = 1,
                    RecipeImg = "https://www.healthyseasonalrecipes.com/wp-content/uploads/2015/01/Italian-Marinara-Mushroom-15.jpg" // done
                },

                new MyRecipeObject
                {
                    RecipeId = 37,
                    RecipeTitle = "Bolognese Penne with Creamy Mushroom Sauce",
                    TagLine = "A Rich and Flavorful Fusion of Bolognese and Creamy Mushrooms",
                    Difficulty = 3,
                    TimeToPrepare = 60,
                    Cuisine = "Italian",
                    RecipeMethod = "1. Cook penne pasta according to package instructions. Drain and set aside.%" +
                                   "2. In a large skillet, heat olive oil and sauté minced garlic and onions until fragrant.%" +
                                   "3. Add ground beef and cook until browned. Drain excess fat.%" +
                                   "4. Stir in tomato paste, crushed tomatoes, Italian herbs, and a pinch of sugar. Simmer for 30-40 minutes to let the flavors meld.%" +
                                   "5. In a separate skillet, melt butter and sauté sliced mushrooms until they release their moisture and turn golden brown.%" +
                                   "6. Add heavy cream and grated Parmesan cheese to the mushrooms. Stir and cook until the sauce thickens.%" +
                                   "7. Combine the Bolognese sauce and creamy mushroom sauce.%" +
                                   "8. Toss the cooked penne pasta in the sauce mixture until well coated.%" +
                                   "9. Serve hot, garnished with fresh basil leaves and an extra sprinkle of Parmesan.",
                    UserId = 1,
                    ForkedFromId = 7,
                    OriginalRecipeId = 1,
                    CuisineId = 1,
                    RecipeImg = "https://www.kitchensanctuary.com/wp-content/uploads/2022/04/One-Pot-Creamy-Bolognese-Pasta-tall-FS-12.webp" // done
                },

                new MyRecipeObject
                {
                    RecipeId = 38,
                    RecipeTitle = "Bolognese Rigatoni Bake",
                    TagLine = "A Comforting Baked Dish with Rigatoni and Bolognese Sauce",
                    Difficulty = 3,
                    TimeToPrepare = 75,
                    Cuisine = "Italian",
                    RecipeMethod = "1. Cook rigatoni pasta according to package instructions until al dente. Drain and set aside.%" +
                                   "2. In a large skillet, heat olive oil and sauté minced garlic and onions until fragrant.%" +
                                   "3. Add ground beef and cook until browned. Drain excess fat.%" +
                                   "4. Stir in tomato paste, crushed tomatoes, Italian herbs, and a pinch of sugar. Simmer for 30-40 minutes to let the flavors meld.%" +
                                   "5. Preheat your oven to 350°F (175°C).%" +
                                   "6. In a baking dish, layer cooked rigatoni and Bolognese sauce. Repeat the layers until all ingredients are used.%" +
                                   "7. Top with shredded mozzarella cheese.%" +
                                   "8. Bake in the preheated oven for 20-25 minutes or until the cheese is melted and bubbly.%" +
                                   "9. Serve hot, garnished with fresh basil leaves.",
                    UserId = 1,
                    ForkedFromId = 7,
                    OriginalRecipeId = 7,
                    CuisineId = 1,
                    RecipeImg = "https://i0.wp.com/www.aspicyperspective.com/wp-content/uploads/2021/08/Creamy-Baked-Rigatoni-Bolognese-23.jpg?resize=650%2C933&ssl=1" // done
                },

                new MyRecipeObject
                {
                    RecipeId = 41,
                    RecipeTitle = "Penne alla Vodka",
                    TagLine = "Creamy Penne Pasta with a Vodka-Infused Tomato Sauce",
                    Difficulty = 3,
                    TimeToPrepare = 45,
                    Cuisine = "Italian",
                    RecipeMethod = "1. Cook penne pasta according to package instructions until al dente. Drain and set aside.%" +
                                   "2. In a large skillet, melt butter and sauté minced garlic until fragrant.%" +
                                   "3. Pour in vodka and cook for a few minutes until reduced.%" +
                                   "4. Add crushed tomatoes, heavy cream, and red pepper flakes. Simmer for 15-20 minutes.%" +
                                   "5. Stir in grated Parmesan cheese and fresh basil.%" +
                                   "6. Toss the cooked penne pasta in the creamy vodka sauce.%" +
                                   "7. Serve hot, garnished with more Parmesan and basil leaves.",
                    UserId = 1,
                    CuisineId = 1,
                    RecipeImg = "https://www.saltandlavender.com/wp-content/uploads/2022/05/penne-alla-vodka-15-720x1080.jpg" // done
                },

                new MyRecipeObject
                {
                    RecipeId = 42,
                    RecipeTitle = "Chicken Alfredo",
                    TagLine = "Creamy Fettuccine Alfredo with Grilled Chicken",
                    Difficulty = 3,
                    TimeToPrepare = 30,
                    Cuisine = "Italian",
                    RecipeMethod = "1. Cook fettuccine pasta according to package instructions. Drain and set aside.%" +
                                   "2. Season chicken breasts with salt, black pepper, and garlic powder. Grill or pan-fry until cooked through. Slice into strips.%" +
                                   "3. In a large skillet, melt butter over medium heat. Add minced garlic and sauté until fragrant.%" +
                                   "4. Pour in heavy cream and bring to a gentle simmer. Cook for a few minutes until it thickens slightly.%" +
                                   "5. Stir in grated Parmesan cheese until the sauce is smooth and creamy.%" +
                                   "6. Toss the cooked fettuccine in the Alfredo sauce until well coated.%" +
                                   "7. Top with grilled chicken strips and chopped fresh parsley.%" +
                                   "8. Serve hot, garnished with extra Parmesan if desired.",
                    UserId = 1,
                    CuisineId = 1,
                    RecipeImg = "https://www.budgetbytes.com/wp-content/uploads/2022/07/Chicken-Alfredo-V3-768x1024.jpg" // done
                },

                new MyRecipeObject
                {
                    RecipeId = 43,
                    RecipeTitle = "Chicken Piccata",
                    TagLine = "Tender Chicken Cutlets in a Lemon-Caper Sauce",
                    Difficulty = 3,
                    TimeToPrepare = 30,
                    Cuisine = "Italian",
                    RecipeMethod = "1. Season chicken cutlets with salt and black pepper. Dredge them in flour, shaking off excess.%" +
                                   "2. In a large skillet, heat olive oil over medium-high heat. Add chicken cutlets and cook until golden brown and cooked through. Remove and set aside.%" +
                                   "3. In the same skillet, add minced garlic and cook until fragrant.%" +
                                   "4. Pour in chicken broth, lemon juice, and capers. Bring to a simmer and cook for a few minutes.%" +
                                   "5. Stir in butter and fresh parsley until the sauce thickens.%" +
                                   "6. Return the cooked chicken cutlets to the skillet, spooning the sauce over them. Cook for an additional minute.%" +
                                   "7. Serve hot, garnished with lemon slices and additional capers if desired.",
                    UserId = 1,
                    CuisineId = 1,
                    RecipeImg = "https://images.themodernproper.com/billowy-turkey/production/posts/2019/Chicken-Picatta-8.jpg?w=800&q=82&fm=jpg&fit=crop&dm=1689343305&s=9818cb3cb4fc509f8932ba868198aae6" // done
                },

                new MyRecipeObject
                {
                    RecipeId = 44,
                    RecipeTitle = "Lasagna",
                    TagLine = "Layered Pasta Dish with Meat Sauce and Creamy Bechamel",
                    Difficulty = 4,
                    TimeToPrepare = 120,
                    Cuisine = "Italian",
                    RecipeMethod = "1. Cook lasagna noodles according to package instructions until al dente. Drain and set aside.%" +
                                   "2. In a large skillet, heat olive oil and sauté minced garlic and onions until translucent.%" +
                                   "3. Add ground beef and Italian sausage. Cook until browned and crumbled. Drain excess fat.%" +
                                   "4. Stir in crushed tomatoes, tomato paste, Italian seasoning, and sugar. Simmer for 20-30 minutes.%" +
                                   "5. In a separate saucepan, melt butter and whisk in flour to create a roux. Slowly add milk, whisking constantly until the sauce thickens. Season with nutmeg, salt, and pepper.%" +
                                   "6. Preheat your oven to 375°F (190°C).%" +
                                   "7. In a large baking dish, layer lasagna noodles, meat sauce, and bechamel sauce. Repeat until all ingredients are used, finishing with a layer of bechamel.%" +
                                   "8. Sprinkle shredded mozzarella and grated Parmesan cheese on top.%" +
                                   "9. Bake in the preheated oven for 30-35 minutes or until the lasagna is bubbling and the cheese is golden brown.%" +
                                   "10. Let it rest for a few minutes before serving.",
                    UserId = 1,
                    CuisineId = 1,
                    RecipeImg = "https://cafedelites.com/wp-content/uploads/2018/01/Mamas-Best-Lasagna-IMAGE-96.jpg" // done
                },

                new MyRecipeObject
                {
                    RecipeId = 45,
                    RecipeTitle = "Mushroom Risotto",
                    TagLine = "Creamy Arborio Rice with Sautéed Mushrooms and Parmesan",
                    Difficulty = 3,
                    TimeToPrepare = 45,
                    Cuisine = "Italian",
                    RecipeMethod = "1. Heat chicken or vegetable broth in a saucepan and keep it warm over low heat.%" +
                                   "2. In a separate large skillet, melt butter over medium heat. Add minced garlic and sauté for about 1 minute until fragrant.%" +
                                   "3. Add Arborio rice to the skillet and cook, stirring constantly, for 2-3 minutes until the rice is lightly toasted.%" +
                                   "4. Pour in white wine and cook, stirring, until the wine is mostly absorbed by the rice.%" +
                                   "5. Begin adding the warm broth, one ladle at a time, to the rice. Stir constantly and allow the liquid to be absorbed before adding more broth. Continue this process until the rice is creamy and cooked to your desired doneness (usually about 18-20 minutes).%" +
                                   "6. While cooking the rice, in another skillet, sauté sliced mushrooms with a bit of olive oil until they are tender and browned.%" +
                                   "7. Stir in grated Parmesan cheese and fresh thyme into the cooked risotto.%" +
                                   "8. Add the sautéed mushrooms to the risotto and gently mix.%" +
                                   "9. Season with salt and black pepper to taste.%" +
                                   "10. Serve hot, garnished with additional Parmesan cheese and fresh thyme leaves.",
                    UserId = 1,
                    CuisineId = 1,
                    RecipeImg = "https://hips.hearstapps.com/del.h-cdn.co/assets/17/35/1504128527-delish-mushroom-risotto.jpg?resize=980:*" // done
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
                    RecipeImg = "https://simply-delicious-food.com/wp-content/uploads/2020/06/Grilled-Pizza-Margherita-5.jpg", // done
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
                    RecipeImg = "https://cookingformysoul.com/wp-content/uploads/2020/02/best-slow-cooked-tacos-al-pastor-min-683x1024.jpg", // done
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
                    RecipeImg = "https://skinnyms.com/wp-content/uploads/2017/03/Teriyaki-Chicken-Bake-Recipe.jpg", // done
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
            new int[] { 242, 363, 634, 564, 396, 663, 675, 375, 34 }, // 33
            new int[] { 594, 242, 363, 638, 543, 422, 396, 663, 249, 335, 34, 31, 347 }, // 34
            new int[] { 662, 242, 363, 638, 543, 422, 396, 663, 249, 335, 34, 31, 347 }, // 35
            new int[] { 634, 242, 363, 638, 543, 422, 396, 663, 249, 355, 34, 31, 347 }, // 36
            new int[] { 683, 242, 363, 638, 543, 422, 396, 663, 249, 355, 34, 31, 347 }, // 37
            new int[] { 688, 242, 363, 638, 543, 422, 396, 663, 249, 355, 34, 31, 347 }, // 38
            new int[] { 683, 5, 363, 212, 396, 27, 34, 347, 378, 349 }, // 41
            new int[] { 671, 528, 5, 363, 27, 34, 375, 378, 349}, // 42
            new int[] { 528, 378, 349, 219, 242, 364, 389, 367, 5, 375 }, // 43
            new int[] { 543, 564, 242, 363, 638, 396, 422, 663, 249, 378, 349 }, // 44
            new int[] { 335, 5, 363, 215, 389, 634, 242, 34, 380, 378, 349 }, // 45

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
            new string[] { "2 tbs", "2 cloves", "200g", "450g", "400g", "2 tsp", "300g", "For garnish", "For garnish" }, // 33
            new string[] { "4 large", "2 tbs", "2 cloves", "1 med size", "500g", "2 tbs", "400g", "2 tsp", "1/2 tsp", "1 cup", "1/2 cup", "1/2 cup", "For garnish" }, // 34
            new string[] { "4 large", "2 tbs", "2 cloves", "1 med size", "500g", "2 tbs", "400g", "2 tsp", "1/2 tsp", "1 cup", "1/2 cup", "1/2 cup", "For garnish" }, // 35
            new string[] { "4 large", "2 tbs", "2 cloves", "1 med size", "500g", "2 tbs", "400g", "2 tsp", "1/2 tsp", "1 cup", "1/2 cup", "1/2 cup", "For garnish" }, // 36
            new string[] { "300g", "2 tbs", "2 cloves", "1 med size", "500g", "2tbs", "400g", "2 tsp", "1/2 tsp", "1 cup", "1/2 cup", "1/2 cup", "For garnish" }, // 37
            new string[] { "300g", "2 tbs", "2 cloves", "1 med size", "500g", "2tbs", "400g", "2 tsp", "1/2 tsp", "1 cup", "1/2 cup", "1/2 cup", "For garnish" }, // 38
            new string[] { "340g", "2 tbs", "3 cloves", "1/4 cup", "1 can", "1 cup", "1/2 cup", "Handful", "To taste", "To taste" }, // 41
            new string[] { "340g", "340g", "1 clove", "4 tbs", "1 cup", "1 cup", "Handful", "To taste", "To taste" }, // 42
            new string[] { "340g", "1/2 tsp", "1/4 tsp", "1/2 cup", "2 tbsp", "2 cloves", "1 cup", "2 pcs", "2 tbsp", "2 tbsp" }, // 43
            new string[] { "450g", "225g", "2 tbsp", "3 cloves", "1 med size", "1 can", "2 tbsp", "2 tsp", "1 tsp", "To taste", "To taste" }, // 44
            new string[] { "1 1/2 cups", "3 tbsp", "2 cloves", "1/2 cup", "4 cups", "225g", "2 tbsp", "1/2", "1 tbsp", "To taste", "To taste" }, // 45

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
