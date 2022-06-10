namespace TimeSheet.CustomExceptions
{
    public class EmptyListException : BaseCustomException
    {
        public EmptyListException() : base() { }

        public EmptyListException(string message) : base(message)
        {
            StatusCode = System.Net.HttpStatusCode.NotFound;
        }
    }
}