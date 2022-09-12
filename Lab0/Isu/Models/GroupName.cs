using Isu.CustomExceptions;

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
        Course = new CourseNumber(groupName[2] - '0');
    }

    public string Name { get; }
    public CourseNumber Course { get; }
    private bool CorrectNameGroup(string groupName)
    {
        return groupName.Length == 6 && char.IsLetter(groupName[0]) && char.IsDigit(groupName[1])
               && char.IsDigit(groupName[2]) && char.IsDigit(groupName[3]) && char.IsDigit(groupName[4])
               && char.IsDigit(groupName[5]);
    }
}