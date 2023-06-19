using System;
using System.Net;

namespace ChatProgramServer
{
    public class TimeRoute : Route
    {
        public override bool CanHandle(string endpoint)
        {
            return endpoint == "/time";
        }

        public override void HandleRequest(HttpListenerRequest request, HttpListenerResponse response)
        {
            string responseString = DateTime.Now.ToString("HH:mm:ss");
            WebServer.WriteResponse(response, responseString);
        }
    }
}
