using Isu.Services;

namespace Isu.Models;

public class GroupName
{
    private string _groupName;

    public GroupName(string groupName)
    {
        if (CorrectNameOfGroup(groupName))
        {
            _groupName = groupName;
        }
        else
        {
            throw new CreateGroupWithInvalidNameException();
        }
    }

    public CourseNumber Course
    {
        get
        {
            var courseNumber = new CourseNumber((int)_groupName[2]);
            return courseNumber;
        }
    }

    private bool CorrectNameOfGroup(string groupName)
    {
        if (groupName.Length == 6 && groupName[0] >= 'A' && groupName[0] <= 'Z' && groupName[1] >= '0' &&
            groupName[1] <= '9' && groupName[2] >= '0' && groupName[2] <= '7' && groupName[3] >= '0' &&
            groupName[3] <= '9' && groupName[4] >= '0' && groupName[4] <= '9' && groupName[5] >= '0' &&
            groupName[5] <= '9')
        {
            return true;
        }

        return false;
    }
}