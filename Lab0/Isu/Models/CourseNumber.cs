using System.Globalization;
using Isu.CustomExceptions;

namespace Isu.Models;

public class CourseNumber
{
    private readonly int _courseNumber;

    public CourseNumber(string groupName)
    {
        if (CharUnicodeInfo.GetDigitValue(groupName[2]) is > 0 and <= 7)
        {
            _courseNumber = CharUnicodeInfo.GetDigitValue(groupName[2]);
        }
        else
        {
            throw CourseNumberException.InvalidCourseNumber(groupName);
        }
    }

    public int GetCourseNumber()
    {
        return _courseNumber;
    }
}