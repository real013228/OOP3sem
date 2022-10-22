using Isu.Extra.Models;

namespace Isu.Extra.DomainExceptions;

public class LessonException : Exception
{
    private LessonException(string msg)
        : base(msg) { }

    public static LessonException InvalidDuration()
    {
        return new LessonException("Invalid duration: lesson should last 90 minutes");
    }

    public static LessonException InvalidOperationException()
    {
        return new LessonException("Invalid operation: invalid arguments had passed");
    }

    public static LessonException InvalidBeginOfLesson()
    {
        return new LessonException($"Invalid begin of lesson: lesson should start from 00:00 01:30 03:00 etc.");
    }
}