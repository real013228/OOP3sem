using Isu.CustomExceptions;
using Isu.Models;
using Isu.Services;

namespace Isu.Entities;

public class Group : IEquatable<Group>
{
    private const int MaxStudentsPerGroup = 3;
    private readonly List<Student> _students = new List<Student>();

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
            throw GroupException.ReachedMaxStudentsPerGroup(this);
        }

        _students.Add(student);
    }

    public void RemoveStudent(Student student)
    {
        if (!_students.Contains(student))
        {
            throw GroupException.CannotRemoveStudent(student);
        }

        _students.Remove(student);
    }

    public bool Equals(Group? other)
        => Name == other?.Name;
}
