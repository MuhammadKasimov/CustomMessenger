namespace CustomMessenger.Service.Exceptions
{
    public class HttpStatusCodeException(int code, string message) : Exception(message)
    {
        public int Code { get { return code; } }
    }
}
