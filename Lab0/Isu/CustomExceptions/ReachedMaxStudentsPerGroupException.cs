using Isu.Entities;

namespace Isu.CustomExceptions;

public class ReachedMaxStudentsPerGroupException : BaseCustomException
{
    public ReachedMaxStudentsPerGroupException(Group group)
        : base($"The group is full: cannot add student in {group.NameOfGroup.Name} group") { }
}