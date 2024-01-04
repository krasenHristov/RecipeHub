using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace OpenSourceRecipes.Utils;
public class MyFoodObject
{
    public int id { get; set; }
    public string name { get; set; }
    public string serving { get; set; }
    public string calories { get; set; }
    public string carbohydrate { get; set; }
    public string sugar { get; set; }
    public string fiber { get; set; }
    public string fat { get; set; }
    public string protein { get; set; }
}

public class ReadFoodFunc
{
    public List<MyFoodObject> ReadFoodFile()
    {
        Console.WriteLine("Reading Foods");
        MyFoodObject[] foodsArray = new MyFoodObject[0];

        string currentDirectory = Environment.CurrentDirectory;
        currentDirectory = Path.Combine(currentDirectory, @"Seeds/data/");
        string[] files = Directory.GetFiles(currentDirectory, "*.txt");

        foreach (string file in files)
        {
            try
            {
                string jsonContent = File.ReadAllText(file);
                MyFoodObject[] foodObjects = JsonConvert.DeserializeObject<MyFoodObject[]>(jsonContent);
                Array.Resize(ref foodsArray, foodsArray.Length + foodObjects.Length);
                Array.Copy(foodObjects, 0, foodsArray, foodsArray.Length - foodObjects.Length, foodObjects.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        Console.WriteLine("Successfully read Foods");
        return new List<MyFoodObject>(foodsArray);
    }
}
