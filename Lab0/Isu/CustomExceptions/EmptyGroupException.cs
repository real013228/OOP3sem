using Isu.Entities;

namespace Isu.CustomExceptions;

public class EmptyGroupException : IsuException
{
    public EmptyGroupException(Group group)
        : base($"The group is empty: cannot remove student from {group.Name.Name}") { }
}