namespace Isu.CustomExceptions;

public class InvalidGroupNameException : IsuException
{
    public InvalidGroupNameException(string groupName)
        : base($"Group name: {groupName} is invalid") { }
}