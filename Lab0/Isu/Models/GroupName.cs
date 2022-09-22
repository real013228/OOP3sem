using System.Globalization;
using Isu.CustomExceptions;
using Isu.Entities;

namespace Isu.Models;

public class GroupName
{
    public GroupName(string groupName)
    {
        if (!CorrectNameGroup(groupName))
        {
            throw new InvalidGroupNameException(groupName);
        }

        Name = groupName;
        Course = new CourseNumber(groupName);
    }

    public string Name { get; }
    public CourseNumber Course { get; }

    private bool CorrectNameGroup(string groupName)
    {
        return groupName.Length == 6 && char.IsLetter(groupName[0]) && groupName.Substring(1, 5).All(char.IsDigit);
    }
}