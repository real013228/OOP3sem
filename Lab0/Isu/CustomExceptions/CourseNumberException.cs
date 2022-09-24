using Isu.Models;

namespace Isu.CustomExceptions;

public class CourseNumberException : IsuException
{
    private CourseNumberException(string msg)
        : base(msg) { }

    public static CourseNumberException InvalidCourseNumber(string groupName)
    {
        return new CourseNumberException($"Course number: {groupName} has an invalid course number");
    }
}