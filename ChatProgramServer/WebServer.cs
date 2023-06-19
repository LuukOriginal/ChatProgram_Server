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
            // Add your route handlers here
            routes.Add(new DefaultRoute());
            routes.Add(new TimeRoute());
            routes.Add(new DataRoute());
            routes.Add(new RegisterRoute());
        }

        public void Start()
        {
            listener = new HttpListener();
            foreach (string prefix in prefixes)
            {
                listener.Prefixes.Add(prefix);
            }
            listener.Start();
            Console.WriteLine("Listening for requests...");

            while (true)
            {
                var context = listener.GetContext();
                var request = context.Request;
                var response = context.Response;

                RouteHandler handler = FindRouteHandler(request.Url.AbsolutePath);
                handler?.HandleRequest(request, response);
            }
        }

        public void Stop()
        {
            if (listener != null && listener.IsListening)
            {
                listener.Stop();
                listener.Close();
            }
        }

        private RouteHandler FindRouteHandler(string endpoint)
        {
            foreach (Route route in routes)
            {
                if (route.CanHandle(endpoint))
                {
                    return new RouteHandler(route);
                }
            }
            return null;
        }

        public static void WriteResponse(HttpListenerResponse response, string responseString)
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
