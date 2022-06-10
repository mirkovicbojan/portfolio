namespace TimeSheet.CustomExceptions
{
    public class EmptyStreamException : BaseCustomException
    {
        public EmptyStreamException() : base() { }

        public EmptyStreamException(string message) : base(message)
        {
            StatusCode = System.Net.HttpStatusCode.PartialContent;
        }
    }
}