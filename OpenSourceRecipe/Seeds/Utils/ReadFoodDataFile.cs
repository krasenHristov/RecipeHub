using Newtonsoft.Json;

namespace OpenSourceRecipes.Utils;
public class MyFoodObject
{
    public int Id { get; set; } = 0;
    public string Name { get; set; } = "";
    public string Serving { get; set; } = "";
    public string Calories { get; set; } = "";
    public string Carbohydrate { get; set; } = "";
    public string Sugar { get; set; } = "";
    public string Fiber { get; set; } = "";
    public string Fat { get; set; } = "";
    public string Protein { get; set; } = "";
}

public class ReadFoodFunc
{
    public List<MyFoodObject> ReadFoodFile()
    {
        Console.WriteLine("Reading Foods");
        MyFoodObject[] foodsArray = Array.Empty<MyFoodObject>();

        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string currentDirectory = Path.GetFullPath(Path.Combine(baseDirectory, @"../../../../"));

        currentDirectory = Path.Combine(currentDirectory, @"OpenSourceRecipe/Seeds/data/");

        string[] files = Directory.GetFiles(currentDirectory, "*.txt");

        foreach (string file in files)
        {
            try
            {
                string jsonContent = File.ReadAllText(file);
                MyFoodObject[]? foodObjects = JsonConvert.DeserializeObject<MyFoodObject[]>(jsonContent);
                Array.Resize(ref foodsArray, foodsArray.Length + foodObjects!.Length);
                Array.Copy(foodObjects, 0, foodsArray, foodsArray.Length - foodObjects.Length, foodObjects.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        Console.WriteLine("Successfully read Foods");
        Console.WriteLine("------------------------");
        return new List<MyFoodObject>(foodsArray);
    }
}
