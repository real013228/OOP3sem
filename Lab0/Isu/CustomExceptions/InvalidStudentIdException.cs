namespace Isu.CustomExceptions;

public class InvalidStudentIdException : BaseCustomException
{
    public InvalidStudentIdException(int id)
        : base($"Student id: {id.ToString()} is invalid") { }
}