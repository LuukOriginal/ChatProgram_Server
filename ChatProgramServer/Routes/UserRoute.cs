using ChatProgramServer.Models;
using System.Net;

namespace ChatProgramServer
{
    public class UserRoute : Route
    {
        private UserModel _user = new UserModel();

        public override bool CanHandle(string endpoint)
        {
            return endpoint == "/user";
        }

        public override async void HandleRequest(HttpListenerRequest request, HttpListenerResponse response)
        {
            string responseString = string.Empty;

            if (request.HttpMethod == "GET")
            {
                using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
                {
                    string data = reader.ReadToEnd();

                    string userName = request.QueryString["username"];
                    string userId = request.QueryString["userid"];

                    if (userName != null && userName.Length > 0)
                    {
                        responseString = await _user.GetUserFromName(userName);
                    }
                    else if(userId != null && userId.Length > 0)
                    {
                        responseString = "Not implemented yet";
                    }
                }
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
