using Isu.CustomExceptions;
using Isu.Entities;

namespace Isu.Models;

public class CourseNumber
{
    private int _courseNumber;

    public CourseNumber(int courseNumber)
    {
        if (courseNumber is > 0 and <= 7)
        {
            _courseNumber = courseNumber;
        }
        else
        {
            throw new InvalidCourseNumberException(courseNumber.ToString());
        }
    }
}