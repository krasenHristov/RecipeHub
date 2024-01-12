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
        };
    }
}
