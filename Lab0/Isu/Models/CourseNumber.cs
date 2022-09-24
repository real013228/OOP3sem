using System.Globalization;
using Isu.CustomExceptions;

namespace Isu.Models;

public record CourseNumber
{
    public int Number { get; }

    public CourseNumber(string groupName)
    {
        if (CharUnicodeInfo.GetDigitValue(groupName[2]) is > 0 and <= 7)
        {
            Number = CharUnicodeInfo.GetDigitValue(groupName[2]);
        }
        else
        {
            throw CourseNumberException.InvalidCourseNumber(groupName);
        }
    }
}