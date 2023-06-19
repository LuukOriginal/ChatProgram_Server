using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using System;

namespace ChatProgramServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            BsonDocument user = new BsonDocument
            {
                { "username", "john.doe" },
                { "email", "john.doe@example.com" },
                { "age", 30 }
            };

            IConfiguration config = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();

            Database db = new Database(config["DatabaseUri"]);
            db.Set("test", user);

            WebServer server = new WebServer("http://localhost:8080/");
            server.RegisterRoutes();
            server.Start();

            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();

            server.Stop();
        }
    }
}
