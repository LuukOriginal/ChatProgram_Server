using System.IO;
using System.Net;

namespace ChatProgramServer
{
    public class RegisterRoute : Route
    {
        public override bool CanHandle(string endpoint)
        {
            return endpoint == "/register";
        }

        public override void HandleRequest(HttpListenerRequest request, HttpListenerResponse response)
        {
            string responseString = string.Empty;

            if (request.HttpMethod == "GET")
            {
                responseString = "hallo, maak een account";
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                responseString = "Method not allowed.";
            }

            WebServer.WriteResponse(response, responseString);
        }
    }
}
