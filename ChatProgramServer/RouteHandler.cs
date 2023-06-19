using System.Net;

namespace ChatProgramServer
{
    public class RouteHandler
    {
        private Route route;

        public RouteHandler(Route route)
        {
            this.route = route;
        }

        public void HandleRequest(HttpListenerRequest request, HttpListenerResponse response)
        {
            route.HandleRequest(request, response);
        }
    }
}
