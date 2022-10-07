using Isu.Models;
using Isu.Services;

namespace Isu.Entities;

public class Group
{
    private List<Student> _students;

    public Group(GroupName name)
    {
        NameOfGroup = name;
        _students = new List<Student>();
    }

    public GroupName NameOfGroup { get; }

    public IReadOnlyList<Student> Students => _students;

    public void AddStudent(Student student)
    {
        _students.Add(student);
    }

    public void RemoveStudent(Student student)
    {
        _students.Remove(student);
    }
}
