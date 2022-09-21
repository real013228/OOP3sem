using Isu.Entities;

namespace Isu.CustomExceptions;

public class GroupAlreadyExistsException : IsuException
{
    public GroupAlreadyExistsException(Group group)
        : base($"Group: {group.Name.Name} this group is already exists in system") { }
}