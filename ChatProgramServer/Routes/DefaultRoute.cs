using System.Net;

namespace ChatProgramServer
{
    public class DefaultRoute : Route
    {
        public override bool CanHandle(string endpoint)
        {
            return endpoint == "/";
        }

        public override void HandleRequest(HttpListenerRequest request, HttpListenerResponse response)
        {
            string responseString = "Hello, World!";
            WebServer.WriteResponse(response, responseString);
        }
    }
}
