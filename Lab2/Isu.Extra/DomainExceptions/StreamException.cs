using Isu.Extra.Entities;

namespace Isu.Extra.DomainExceptions;

public class StreamException : Exception
{
    private StreamException(string msg)
        : base(msg) { }

    public static StreamException InvalidStudent(StudentExtra student)
    {
        return new StreamException($"Invalid student: {student.Name} has not enrolled at this extra study");
    }

    public static StreamException InvalidRequest(StudentExtra student)
    {
        return new StreamException($"Invalid student: There is not {student.Name} in group");
    }
}