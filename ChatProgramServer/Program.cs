using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using System;

namespace ChatProgramServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            WebServer server = new WebServer("http://localhost:8080/");
            server.RegisterRoutes();
            server.Start();

            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();

            server.Stop();
        }
    }
}
