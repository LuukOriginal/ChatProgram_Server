using System.IO;
using System.Net;

namespace ChatProgramServer
{
    public class ExampleRoute : Route
    {
        public override bool CanHandle(string endpoint)
        {
            //Returns if the route has the correct endpoint
            return endpoint == "example";
        }

        public override void HandleRequest(HttpListenerRequest request, HttpListenerResponse response)
        {
            string responseString = string.Empty;
            
            if (request.HttpMethod == "GET") //handles the GET requests to this endpoint
            {
                responseString = "This is an Example!";
            }
            else //Handles the requests not specified in this endpoint
            {
                response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                responseString = "Method not allowed.";
            }

            WebServer.WriteResponse(response, responseString);
        }
    }
}
