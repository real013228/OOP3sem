using Isu.CustomExceptions;
using Isu.Entities;
using Isu.Extra.Entities;

namespace Isu.Extra.DomainExceptions;

public class StudentExtraException : Exception
{
    private StudentExtraException(string msg)
        : base(msg)
    {
    }

    public static StudentExtraException InvalidEnrollReachedMax()
    {
        return new StudentExtraException($"Invalid operation enrolling: student already has 2 extra studies");
    }

    public static StudentExtraException InvalidRemovingEnrollToExtraStudy(ExtraStudy extraStudy)
    {
        return new StudentExtraException(
            $"Invalid operation enrolling: student has not enroll to this extra study {extraStudy.ExtraStudyName}");
    }

    public static StudentExtraException InvalidExtraStudy(MegaFaculty megaFaculty)
    {
        return new StudentExtraException(
            $"Invalid enrolling: student already is a member of this mega-faculty {megaFaculty.Name}");
    }

    public static StudentExtraException InvalidExtraStudy()
    {
        return new StudentExtraException("Invalid enrolling: there is intersection with main schedule");
    }

    public static StudentExtraException EmptyStreamInExtraStudy()
    {
        return new StudentExtraException("Invalid extra study: there is no any stream at this extra study");
    }
}