using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace OpenSourceRecipes.Utils;
public class MyUserObject
{
    public string Username { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string ProfileImg { get; set; }
    public string Status { get; set; }
    public string Bio { get; set; }
}

public class ReadUserFunc
{
    public List<MyUserObject> ReadUserFile()
    {   
        string currentDirectory = Environment.CurrentDirectory;
        string filePath = @"Seeds/data/users.txt";
        filePath = Path.Combine(currentDirectory, filePath);

        try
        {
            string jsonContent = File.ReadAllText(filePath);
            MyUserObject[] usersArray = JsonConvert.DeserializeObject<MyUserObject[]>(jsonContent);
            return usersArray.ToList();
        }
        catch (Exception ex)
        {  
            Console.WriteLine($"An error occurred: {ex.Message}"); 
            return new List<MyUserObject>();
        }
    }
}
