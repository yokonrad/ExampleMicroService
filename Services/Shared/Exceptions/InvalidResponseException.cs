namespace Shared.Exceptions
{
    public class InvalidResponseException : Exception
    {
        public InvalidResponseException()
        { }

        public InvalidResponseException(string? message) : base(message)
        { }

        public InvalidResponseException(string? message, Exception? innerException) : base(message, innerException)
        { }
    }
}