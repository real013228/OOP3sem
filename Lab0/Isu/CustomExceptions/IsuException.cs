namespace Isu.CustomExceptions;

public abstract class IsuException : Exception
{
    protected IsuException() { }

    protected IsuException(string message)
        : base(message) { }

    protected IsuException(string message, Exception inner)
        : base(message, inner) { }
}