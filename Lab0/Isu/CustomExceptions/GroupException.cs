using Isu.Entities;

namespace Isu.CustomExceptions;

public class GroupException : IsuException
{
    private GroupException(string msg)
        : base(msg) { }

    public static GroupException ReachedMaxStudentsPerGroup(Group group)
    {
        return new GroupException($"The group is full: cannot add student in {group.Name.Name} group");
    }

    public static GroupException StudentHasGroup(Student student)
    {
        return new GroupException($"Cannot add to the group: the student {student.Name} already has a group");
    }

    public static GroupException CannotRemoveStudent(Student student)
    {
        return new GroupException($"Invalid operation: cannot remove student {student.Name}");
    }
}