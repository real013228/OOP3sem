namespace Isu.CustomExceptions;

public class InvalidStudentIdException : IsuException
{
    public InvalidStudentIdException(int id)
        : base($"Student id: {id.ToString()} is invalid") { }
}