using Isu.Extra.Entities;

namespace Isu.Extra.DomainExceptions;

public class ExtraStudyException : Exception
{
    private ExtraStudyException(string msg)
        : base(msg) { }

    public static ExtraStudyException InvalidRequestException()
    {
        return new ExtraStudyException($"Invalid request: there is not any free stream");
    }
}