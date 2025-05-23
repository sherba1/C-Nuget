using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

namespace MyApp
{
    class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
    }

    class Admin : User
    {
        public string Role { get; set; } = "Admin";
    }

    class RegularUser : User
    {
        public string Role { get; set; } = "User";
    }

    class Main
    {
        static void Main(string[] args)
        {
            string jsonPath = "user.json";
            List<User> users;
            if (File.Exists(jsonPath))
            {
                string jsonData = File.ReadAllText(jsonPath);
                users = JsonConvert.DeserializeObject<List<User>>(jsonData);
            }
            else
            {
                users = new List<User>
                {
                    new User { Name = "John Doe", Age = 30, City = "New York" },
                    new User { Name = "Jane Smith", Age = 28, City = "Los Angeles" }
                };
                File.WriteAllText(jsonPath, JsonConvert.SerializeObject(users, Formatting.Indented));
            }

            users.Add(new User { Name = "Alice Johnson", Age = 35, City = "Chicago" });
            File.WriteAllText(jsonPath, JsonConvert.SerializeObject(users, Formatting.Indented));

            Console.WriteLine("All Users:");
            foreach (var u in users)
            {
                Console.WriteLine($"Name: {u.Name}, Age: {u.Age}, City: {u.City}");
            }

            var specialUsers = new List<User>
            {
                new Admin { Name = "Super Admin", Age = 40, City = "Boston" },
                new RegularUser { Name = "Normal User", Age = 22, City = "Miami" }
            };
            string typesPath = "user_types.json";
            File.WriteAllText(typesPath, JsonConvert.SerializeObject(specialUsers, Formatting.Indented));

            var loadedSpecial = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(typesPath));
            Console.WriteLine("\nSpecialized Users:");
            foreach (var su in loadedSpecial)
            {
                string role = su is Admin ? "Admin" : "User";
                Console.WriteLine($"Role: {role}, Name: {su.Name}, Age: {su.Age}, City: {su.City}");
            }

            // Bonus
            if (File.Exists("user.xml"))
            {
                Console.WriteLine("\nXML Users from user.xml:");
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("user.xml");
                XmlNodeList nodes = xmlDoc.SelectNodes("/Users/User");
                foreach (XmlNode node in nodes)
                {
                    Console.WriteLine($"Name: {node["Name"].InnerText}, Age: {node["Age"].InnerText}, City: {node["City"].InnerText}");
                }
            }
        }
    }
}
