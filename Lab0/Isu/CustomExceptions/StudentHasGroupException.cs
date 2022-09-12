using Isu.Entities;

namespace Isu.CustomExceptions;

public class StudentHasGroupException : BaseCustomException
{
    public StudentHasGroupException(Student student)
        : base($"Cannot add to the group: the student {student.Name} already has a group") { }
}