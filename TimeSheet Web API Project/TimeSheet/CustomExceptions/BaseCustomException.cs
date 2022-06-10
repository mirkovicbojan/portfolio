using System.Net;

namespace TimeSheet.CustomExceptions
{
    public class BaseCustomException : Exception
    {
        public HttpStatusCode StatusCode;

        public BaseCustomException() {}

        public BaseCustomException(string message) : base(message) {}
    }
}