using Isu.Models;

namespace Isu.CustomExceptions;

public class GroupNameException : IsuException
{
    private GroupNameException(string msg)
        : base(msg) { }

    public static GroupNameException InvalidGroupName(string groupName)
    {
        return new GroupNameException($"Group name: {groupName} is invalid");
    }
}