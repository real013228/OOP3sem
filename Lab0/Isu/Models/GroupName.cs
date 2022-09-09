namespace Isu.Models;

public class GroupName
{
    private string _groupName;

    public GroupName(string groupName)
    {
        _groupName = groupName;
    }

    public CourseNumber Course
    {
        get
        {
            var courseNumber = new CourseNumber((int)_groupName[2]);
            return courseNumber;
        }
    }
}