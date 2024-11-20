namespace Shared.Exceptions
{
    public class InvalidDatabaseResultException : Exception
    {
        public InvalidDatabaseResultException()
        { }

        public InvalidDatabaseResultException(string? message) : base(message)
        { }

        public InvalidDatabaseResultException(string? message, Exception? innerException) : base(message, innerException)
        { }
    }
}