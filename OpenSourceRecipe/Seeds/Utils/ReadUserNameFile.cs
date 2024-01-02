using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace OpenSourceRecipes.Utils;
    public class MyUserObject : IEnumerable<string>
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string ProfileImg { get; set; }
        public bool Status { get; set; }
        public string Bio { get; set; }

        // Implementing IEnumerable<string> interface
        public IEnumerator<string> GetEnumerator()
        {
            yield return Username;
            yield return Name;
            yield return Password;
            yield return ProfileImg;
            yield return Status.ToString();
            yield return Bio;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

public class ReadUserFunc
{
    public List<MyUserObject> ReadUserFile()
    {
        string filePath = "../../data/users.txt";
        
        try
        {
            string jsonContent = File.ReadAllText(filePath);
            List<MyUserObject> ConvertedFile = JsonConvert.DeserializeObject<List<MyUserObject>>(jsonContent);
            return ConvertedFile;
        }
        catch (Exception ex)
        {  
            Console.WriteLine($"An error occurred: {ex.Message}"); 
            return new List<MyUserObject>();
        }
    }
}