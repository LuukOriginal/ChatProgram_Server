using System;
using System.Collections.Generic;
using System.Net;

namespace ChatProgramServer
{
    public class WebServer
    {
        private HttpListener listener;
        private string[] prefixes;
        private List<Route> routes;

        public WebServer(params string[] prefixes)
        {
            this.prefixes = prefixes;
            this.routes = new List<Route>();
        }

        public void RegisterRoutes()
        {
            // Route Handlers
            routes.Add(new DefaultRoute());
            routes.Add(new TimeRoute());
            routes.Add(new MessageRoute());
            routes.Add(new UserRoute());
        }

        public void Start()
        {
            //Sets up an httplistener to listen for incomming requests
            listener = new HttpListener();
            foreach (string prefix in prefixes)
            {
                listener.Prefixes.Add(prefix);
            }
            listener.Start();
            Console.WriteLine("Listening for requests...");

            while (true)
            {
                try
                {
                    var context = listener.GetContext();
                    var request = context.Request;
                    var response = context.Response;

                    tempLog($"{request.HttpMethod} | {request.Url.AbsolutePath} | {request.RemoteEndPoint.ToString()}");

                    //Checks if any routes match with the specified route, and if so, handle them.
                    RouteHandler handler = FindRouteHandler(request.Url.AbsolutePath);
                    handler?.HandleRequest(request, response);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[Error]: " + ex.Message);
                }
            }
        }

        public void tempLog(string message)
        {
            //Temporary function for logging something with the time included
            string time = DateTime.Now.ToString("HH:mm:ss");
            Console.WriteLine($"[{time}] {message}");
        }

        public void Stop() //stops the server
        {
            if (listener != null && listener.IsListening)
            {
                listener.Stop();
                listener.Close();
            }
        }

        private RouteHandler FindRouteHandler(string endpoint) //checks what route matches with the specified endpoint
        {
            foreach (Route route in routes)
            {
                if (route.CanHandle(endpoint.ToLower()))
                {
                    return new RouteHandler(route);
                }
            }
            return null;
        }

        public static void WriteResponse(HttpListenerResponse response, string responseString) //function for responding back to the request
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentType = "text/plain";
            response.ContentLength64 = buffer.Length;

            var output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }
    }
}
