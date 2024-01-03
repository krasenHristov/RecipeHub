using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace OpenSourceRecipes.Utils;
public class MyFoodObject
{
    public int id { get; set; }
    public string serving { get; set; }
    public string calories { get; set; }
    public string carbohydrate { get; set; }
    public string sugar { get; set; }
    public string fiber { get; set; }
    public string fat { get; set; }
    public string protein { get; set; }
    public string name { get; set; }
}

public class ReadFoodFunc
{
    public List<MyFoodObject> ReadFoodFile()
    {           
        string currentDirectory = Environment.CurrentDirectory;
        string[] files = Directory.GetFiles(Environment.CurrentDirectory, "*.txt");
        foreach (string file in files)
        {
            Console.WriteLine(file);
        }

        string filePath = @"Seeds/data/users.txt";
        filePath = Path.Combine(currentDirectory, filePath);

        try
        {
            string jsonContent = File.ReadAllText(filePath);
            MyFoodObject[] usersArray = JsonConvert.DeserializeObject<MyFoodObject[]>(jsonContent);
            return usersArray.ToList();
        }
        catch (Exception ex)
        {  
            Console.WriteLine($"An error occurred: {ex.Message}"); 
            return new List<MyFoodObject>();
        }
    }
}
