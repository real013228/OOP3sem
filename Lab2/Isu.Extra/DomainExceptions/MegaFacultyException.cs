using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Models;

namespace Isu.Extra.DomainExceptions;

public class MegaFacultyException : Exception
{
    private MegaFacultyException(string msg)
        : base(msg) { }

    public static MegaFacultyException InvalidGroupName(GroupName groupName)
    {
        return new MegaFacultyException($"Invalid group name: {groupName.Name} there is no group with same name");
    }
}