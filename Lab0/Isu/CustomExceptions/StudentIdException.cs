using Isu.Models;

namespace Isu.CustomExceptions;

public class StudentIdException : IsuException
{
    private StudentIdException(string msg)
        : base(msg) { }

    public static StudentIdException InvalidId(int id)
    {
        return new StudentIdException($"Invalid id: {id.ToString()} should be between 100000 and 900000");
    }
}