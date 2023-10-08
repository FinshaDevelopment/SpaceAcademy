using Newtonsoft.Json.Linq;

namespace NASAProj.Service.Exceptions
{
    public class HttpStatusCodeException : Exception
    {
        public int StatusCode { get; set; }
        public HttpStatusCodeException(int statusCode, string message) : base(message)
        {
            this.StatusCode = statusCode;
        }
    }
}
