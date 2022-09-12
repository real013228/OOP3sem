using Isu.CustomExceptions;
using Isu.Models;
using Isu.Services;

namespace Isu.Entities;

public class Group
{
    private const int MaxStudentsPerGroup = 3;
    private List<Student> _students = new List<Student>();

    public Group(GroupName name)
    {
        NameOfGroup = name;
    }

    public GroupName NameOfGroup { get; }

    public IReadOnlyList<Student> Students
    {
        get => _students;
    }

    public void AddStudent(Student student)
    {
        if (_students.Count >= MaxStudentsPerGroup)
        {
            throw new ReachedMaxStudentsPerGroupException(this);
        }

        if (!string.IsNullOrEmpty(student.Group.NameOfGroup.Name) && _students.Contains(student))
        {
            throw new StudentHasGroupException(student);
        }

        _students.Add(student);
    }

    public void RemoveStudent(Student student)
    {
        if (_students.Any())
        {
            _students.Add(student);
        }
        else
        {
            throw new ReachedMaxStudentsPerGroupException(this);
        }
    }
}
