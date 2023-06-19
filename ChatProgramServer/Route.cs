using System.Net;

namespace ChatProgramServer
{
    public abstract class Route
    {
        public abstract bool CanHandle(string endpoint);
        public abstract void HandleRequest(HttpListenerRequest request, HttpListenerResponse response);
    }
}
