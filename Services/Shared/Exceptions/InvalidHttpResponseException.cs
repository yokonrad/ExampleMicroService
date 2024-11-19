namespace Shared.Exceptions
{
    public class InvalidHttpResponseException : Exception
    {
        public InvalidHttpResponseException()
        { }

        public InvalidHttpResponseException(string? message) : base(message)
        { }

        public InvalidHttpResponseException(string? message, Exception? innerException) : base(message, innerException)
        { }
    }
}