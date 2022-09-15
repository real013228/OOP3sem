namespace Isu.CustomExceptions;

public class InvalidIdValueException : BaseCustomException
{
    public InvalidIdValueException(string value)
        : base($"Invalid value: {value} should be between 0 and 900000") { }
}
