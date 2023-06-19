using System.IO;
using System.Net;

namespace ChatProgramServer
{
    public class DataRoute : Route
    {
        public override bool CanHandle(string endpoint)
        {
            return endpoint == "/data";
        }

        public override void HandleRequest(HttpListenerRequest request, HttpListenerResponse response)
        {
            string responseString = string.Empty;

            if (request.HttpMethod == "POST")
            {
                using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
                {
                    string requestBody = reader.ReadToEnd();
                    responseString = $"Received data: {requestBody}";
                }
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
