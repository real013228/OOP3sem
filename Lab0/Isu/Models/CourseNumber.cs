using System.Globalization;
using Isu.CustomExceptions;
using Isu.Entities;

namespace Isu.Models;

public class CourseNumber
{
    private readonly int _courseNumber;

    public CourseNumber(string group)
    {
        if (CharUnicodeInfo.GetDigitValue(group[2]) is > 0 and <= 7)
        {
            _courseNumber = CharUnicodeInfo.GetDigitValue(group[2]);
        }
        else
        {
            throw new InvalidCourseNumberException(group.ToString());
        }
    }

    public int GetCourseNumber()
    {
        return _courseNumber;
    }
}