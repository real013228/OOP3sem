using Isu.CustomExceptions;
using Isu.Models;

namespace Isu.Entities;

public class Student
{
    public Student(StudentId id, string name, Group group)
    {
        Id = id;
        Name = name;
        Group = group;
        group.AddStudent(this);
    }

    public string Name { get; }
    public StudentId Id { get; }
    public Group Group { get; private set; }

    public void ChangeGroup(Group newGroup)
    {
        newGroup.AddStudent(this);
        Group.RemoveStudent(this);
        Group = newGroup;
    }
}