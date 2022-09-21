namespace Isu.CustomExceptions;

public class InvalidCourseNumberException : IsuException
{
        public InvalidCourseNumberException(string courseNumber)
                : base($"Course number: {courseNumber} is invalid") { }
}