namespace CleanArchitecture.Domain.Exceptions;

public sealed class InvalidIdException : Exception
{
    public InvalidIdException() : base() { }
    public InvalidIdException(string message) : base(message) { }
}
