using System;
using System.Net;
using System.Threading;

class WebServer
{
    private HttpListener listener;

    public void Start(string baseUrl)
    {
        listener = new HttpListener();
        listener.Prefixes.Add(baseUrl);
        listener.Start();
        Console.WriteLine("Web server started. Listening for requests...");

        // Handle incoming requests
        while (true)
        {
            // Get the context of the incoming request
            HttpListenerContext context = listener.GetContext();

            // Process the request in a separate thread
            ThreadPool.QueueUserWorkItem(ProcessRequest, context);
        }
    }

    public void Stop()
    {
        listener.Stop();
        listener.Close();
    }

    private void ProcessRequest(object state)
    {
        // Get the context from the state object
        HttpListenerContext context = (HttpListenerContext)state;

        // Extract the request and response objects
        HttpListenerRequest request = context.Request;
        HttpListenerResponse response = context.Response;

        // Create a response message
        string responseString = "<html><body><h1>Hello, World!</h1></body></html>";
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);

        // Set the content type and length of the response
        response.ContentType = "text/html";
        response.ContentLength64 = buffer.Length;

        // Write the response
        System.IO.Stream output = response.OutputStream;
        output.Write(buffer, 0, buffer.Length);

        // Close the output stream
        output.Close();
    }
}