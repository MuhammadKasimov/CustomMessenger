using System.Net;

namespace CustomMessengerBlazor.Exceptions
{
    public class HttpStatusCodeException(HttpStatusCode code, string message) : Exception(message)
    {
        public HttpStatusCode Code { get { return code; } }
    }
}
