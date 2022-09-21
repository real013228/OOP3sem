namespace Isu.CustomExceptions;

public class InvalidIdValueException : IsuException
{
    public InvalidIdValueException(string value)
        : base($"Invalid value: {value} should be between 0 and 900000") { }
}
