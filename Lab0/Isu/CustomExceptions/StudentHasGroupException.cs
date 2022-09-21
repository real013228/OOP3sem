using Isu.Entities;

namespace Isu.CustomExceptions;

public class StudentHasGroupException : IsuException
{
    public StudentHasGroupException(Student student)
        : base($"Cannot add to the group: the student {student.Name} already has a group") { }
}