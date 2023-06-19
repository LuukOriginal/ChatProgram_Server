using ChatProgramServer.Models;
using System.IO;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace ChatProgramServer
{
    public class UserRoute : Route
    {
        private UserModel _user = new UserModel();

        public override bool CanHandle(string endpoint)
        {
            return endpoint == "/User";
        }

        public override void HandleRequest(HttpListenerRequest request, HttpListenerResponse response)
        {
            string responseString = string.Empty;

            if (request.HttpMethod == "GET")
            {
                responseString = "hier is een account";
            }
            else if (request.HttpMethod == "POST")
            {
                using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
                {
                    string data = reader.ReadToEnd();
                    responseString = _user.CreateUser(data);
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
