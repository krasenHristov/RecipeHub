namespace OpenSourceRecipes.Seeds;

public class CuisinesData
{
    public IEnumerable<MyCuisineObject> GetCuisinesData()
    {
        return new[]
        {

            new MyCuisineObject
            {
                CuisineId = 1,
                CuisineName = "Italian",
                Description = "Explore the world of Italian culinary artistry by sharing your favorite Italian recipes. Whether it's homemade pasta, rich sauces, or traditional desserts, our platform is the perfect place to showcase and discover the magic of Italian cooking",
                CuisineImg = "https://images.unsplash.com/photo-1556761223-4c4282c73f77?q=80&w=1365&auto=format&fit=[…]3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
            },

            new MyCuisineObject
            {
                CuisineId = 2,
                CuisineName = "Mexican",
                Description = "Share the spice and flair of Mexican cuisine by contributing your mouthwatering Mexican recipes. From sizzling fajitas to savory enchiladas, our recipe-sharing platform celebrates the vibrant and flavorful world of Mexican cooking.",
                CuisineImg = "https://images.unsplash.com/photo-1565299585323-38d6b0865b47?q=80&w=1380&auto=format&f[…]3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
            },

            new MyCuisineObject
            {
                CuisineId = 3,
                CuisineName = "Japanese",
                Description = "Dive into the depths of authentic Japanese flavors by adding your cherished Japanese recipes. Whether it's sushi rolls, comforting ramen, or elegant tempura dishes, our recipe collection is your gateway to the exquisite tastes of Japan.",
                CuisineImg = "https://images.unsplash.com/photo-1569718212165-3a8278d5f624?q=80&w=1480&auto=format&f[…]3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
            },

            new MyCuisineObject
            {
                CuisineId = 4,
                CuisineName = "British",
                Description = "Embrace the rich tapestry of British culinary heritage by contributing your beloved British recipes. From hearty classics like Shepherd's Pie and Fish and Chips to elegant afternoon tea treats, our recipe platform pays tribute to the diverse and comforting flavors of British cooking. Share and savor the taste of Great Britain with fellow food enthusiasts.",
                CuisineImg = "https://qph.cf2.quoracdn.net/main-qimg-4b9b9506613bba9c5503a69efc60ca51-lq"
            },

            new MyCuisineObject
            {
                CuisineId = 5,
                CuisineName = "French",
                Description = "Embark on a culinary journey to the heart of France by sharing your cherished French recipes. From decadent pastries like croissants and éclairs to classic dishes like Coq au Vin and Ratatouille, our recipe platform invites you to experience the sophistication and flavors of French gastronomy. Bon appétit!",
                CuisineImg = "https://www.japantimes.co.jp/uploads/imported_images/uploads/2020/02/p2-spotlight-hotels-a-20200228.jpg"
            },

            new MyCuisineObject
            {
                CuisineId = 6,
                CuisineName = "Spanish",
                Description = "Immerse yourself in the vibrant and diverse flavors of Spanish cuisine by contributing your favorite Spanish recipes. From mouthwatering paellas and tapas to delightful churros and gazpacho, our recipe platform celebrates the essence of Spain's culinary heritage. ¡Buen provecho!",
                CuisineImg = "https://spanishsabores.com/wp-content/uploads/2020/05/Seafood-Paella-1851-Blog.jpg"
            },

            new MyCuisineObject
            {
                CuisineId = 7,
                CuisineName = "Chinese",
                Description = "Delve into the diverse and flavorful world of Chinese cuisine by sharing your treasured Chinese recipes. From savory stir-fries and dim sum delights to the art of noodle-making and the secrets of Peking duck, our recipe platform offers a taste of the culinary traditions of China. 享受您的美食!",
                CuisineImg = "https://as1.ftcdn.net/v2/jpg/01/15/26/28/1000_F_115262838_Qdfwviyw9ATjw0TNnky95RjvKoQXprj5.jpg"
            },

            new MyCuisineObject
            {
                CuisineId = 8,
                CuisineName = "Indian",
                Description = "Embark on a spicy and aromatic culinary adventure through the diverse flavors of Indian cuisine. Share your favorite Indian recipes, from creamy curries and tandoori delights to fragrant biryanis and sweet desserts. Our recipe platform invites you to savor the rich traditions and vibrant tastes of India. आपके खाने का स्वाद लूटें!",
                CuisineImg = "https://images.unsplash.com/photo-1585937421612-70a008356fbe?q=80&w=1936&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
            },

            new MyCuisineObject
            {
                CuisineId = 9,
                CuisineName = "Vegetarian",
                Description = "Celebrate the beauty of plant-based cuisine by contributing your favorite vegetarian recipes. From fresh salads and hearty soups to innovative meatless creations and satisfying desserts, our recipe platform is a haven for those who appreciate the flavors and health benefits of vegetarian cooking. Embrace the green goodness!",
                CuisineImg = "https://images.unsplash.com/photo-1599020792689-9fde458e7e17?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8dmVnZXRhcmlhbiUyMGZvb2R8ZW58MHx8MHx8fDA%3D"
            },

            new MyCuisineObject
            {
                CuisineId = 10,
                CuisineName = "Thai",
                Description = "Experience the bold and exotic flavors of Thai cuisine by sharing your favorite Thai recipes. From aromatic curries and spicy stir-fries to refreshing salads and sweet mango sticky rice, our recipe platform invites you to savor the culinary treasures of Thailand. ทานให้อร่อย!",
                CuisineImg = "https://images.unsplash.com/photo-1455619452474-d2be8b1e70cd?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
            },

            new MyCuisineObject
            {
                CuisineId = 11,
                CuisineName = "Greek",
                Description = "Indulge in the Mediterranean delights of Greek cuisine by contributing your cherished Greek recipes. From hearty gyros and moussaka to crispy spanakopita and baklava, our recipe platform celebrates the flavors and traditions of Greece. Καλή όρεξη!",
                CuisineImg = "https://s3.ivisa.com/website-assets/blog/best-greek-food.webp"
            },

            new MyCuisineObject
            {
                CuisineId = 12,
                CuisineName = "Vietnamese",
                Description = "Delight in the fresh and aromatic flavors of Vietnamese cuisine by contributing your beloved Vietnamese recipes. From flavorful pho and crispy spring rolls to vibrant banh mi and sweet Vietnamese coffee, our recipe platform captures the essence of Vietnamese cooking. Mời bạn thưởng thức!",
                CuisineImg = "https://media.istockphoto.com/id/1253793642/photo/traditional-asian-dishes-for-family-dinner.jpg?s=612x612&w=0&k=20&c=6v0MQjVNaNnSIPaV-wU6lZzM0wsLlzeLGYV6PRW1OXQ="
            },

            new MyCuisineObject
            {
                CuisineId = 14,
                CuisineName = "Korean",
                Description = "Discover the bold and savory flavors of Korean cuisine by sharing your favorite Korean recipes. From spicy kimchi and sizzling barbecue to comforting bibimbap and crispy Korean fried chicken, our recipe platform brings the tastes of Korea to your kitchen. 맛있게 드세요!",
                CuisineImg = "https://www.ragus.co.uk/wp-content/uploads/2022/12/Korean_cuisine_01_ss_1588959085_1295x715px.jpg"
            },

            new MyCuisineObject
            {
                CuisineId = 15,
                CuisineName = "Moroccan",
                Description = "Embark on a journey to the exotic world of Moroccan cuisine by contributing your cherished Moroccan recipes. From aromatic tagines and flavorful couscous to sweet pastries like baklava and ma'amoul, our recipe platform invites you to savor the rich traditions of Morocco. بالصحة والعافية!",
                CuisineImg = "https://www.tasteofhome.com/wp-content/uploads/2018/02/shutterstock_177937613.jpg?resize=768%2C512"
            },

            new MyCuisineObject
            {
                CuisineId = 16,
                CuisineName = "Brazilian",
                Description = "Experience the diverse and vibrant flavors of Brazilian cuisine by sharing your favorite Brazilian recipes. From succulent churrasco and feijoada to tropical fruit desserts and caipirinhas, our recipe platform captures the essence of Brazil's culinary culture. Aproveite!",
                CuisineImg = "https://www.ciee.org/sites/default/files/styles/960w/public/blog/2018-11/feijoada.jpg?itok=WkgGq4mf"
            },

            new MyCuisineObject
            {
                CuisineId = 17,
                CuisineName = "Lebanese",
                Description = "Savor the rich and aromatic flavors of Lebanese cuisine by contributing your beloved Lebanese recipes. From mouthwatering shawarma and creamy hummus to flaky baklava and aromatic falafel, our recipe platform celebrates the culinary traditions of Lebanon. بالهناء والشفاء!",
                CuisineImg = "https://xyuandbeyond.com/wp-content/uploads/2022/06/dish-meal-food-salad-mediterranean-produce-1050376-pxhere.com_-1024x682.jpg"
            }
        };
    }
}
