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
        Name = name;
    }

    public GroupName Name { get; }

    public IReadOnlyList<Student> Students => _students;

    public void AddStudent(Student student)
    {
        if (_students.Count >= MaxStudentsPerGroup)
        {
            throw new ReachedMaxStudentsPerGroupException(this);
        }

        if (!string.IsNullOrEmpty(student.Group.Name.Name) && _students.Contains(student))
        {
            throw new StudentHasGroupException(student);
        }

        _students.Add(student);
    }

    public void RemoveStudent(Student student)
    {
        if (_students.Any())
        {
            _students.Remove(student);
        }
        else
        {
            throw new ReachedMaxStudentsPerGroupException(this);
        }
    }
}
