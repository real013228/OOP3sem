namespace Isu.CustomExceptions;

public class InvalidGroupNameException : BaseCustomException
{
    public InvalidGroupNameException(string groupName)
        : base($"Group name: {groupName} is invalid") { }
}