namespace TimeSheet.CustomExceptions
{
    public class InvalidObjectParamsException : BaseCustomException
    {
        public InvalidObjectParamsException() : base() { }

        public InvalidObjectParamsException(string message) : base(message)
        {
            StatusCode = System.Net.HttpStatusCode.BadRequest;
        }
    }
}