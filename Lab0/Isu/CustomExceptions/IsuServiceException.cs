using Isu.Entities;
using Isu.Models;

namespace Isu.CustomExceptions;

public class IsuServiceException : IsuException
{
    private IsuServiceException(string msg)
        : base(msg) { }

    public static IsuServiceException GroupAlreadyExists(GroupName groupName)
    {
        return new IsuServiceException($"Group: {groupName.Name} this group is already exists in system");
    }

    public static IsuServiceException CannotFindStudent(int id)
    {
        return new IsuServiceException($"Student id: {id.ToString()} cannot find student with identical id");
    }
}