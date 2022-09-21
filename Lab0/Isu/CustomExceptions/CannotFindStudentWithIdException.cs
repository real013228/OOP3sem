namespace Isu.CustomExceptions;

public class CannotFindStudentWithIdException : IsuException
{
    public CannotFindStudentWithIdException(int id)
        : base($"Student id: {id.ToString()} cannot find student with identical id") { }
}