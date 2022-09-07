using Isu.Models;
using Isu.Services;

namespace Isu.Entities;

public class Group
{
    private GroupName _nameOfGroup;
    private List<Student> _students;
    public Group(GroupName name)
    {
        _nameOfGroup = name;
        _students = new List<Student>();
    }
}
