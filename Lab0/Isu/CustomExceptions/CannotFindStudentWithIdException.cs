namespace Isu.CustomExceptions;

public class CannotFindStudentWithIdException : BaseCustomException
{
    public CannotFindStudentWithIdException(int id)
        : base($"Student id: {id.ToString()} cannot find student with identical id") { }
}