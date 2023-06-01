using System;
class Program
{
    public static void Main(string[] args)
    {
        string baseUrl = "http://localhost:8080/";
        WebServer webServer = new WebServer();

        try
        {
            webServer.Start(baseUrl);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            webServer.Stop();
        }
    }
}