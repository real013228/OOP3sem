namespace Isu.CustomExceptions;

public class InvalidCourseNumberException : BaseCustomException
{
        public InvalidCourseNumberException(string courseNumber)
                : base($"Course number: {courseNumber} is invalid") { }
}