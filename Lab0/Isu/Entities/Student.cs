using Isu.CustomExceptions;
using Isu.Models;

namespace Isu.Entities;

public class Student
{
    public Student(int id, string name, Group group)
    {
        var studentId = new StudentId(id);
        Id = studentId;
        Name = name;
        Group = group;
        group.AddStudent(this);
    }

    public string Name { get; }
    public StudentId Id { get; }

    public Group Group { get; private set; }

    public void ChangeGroup(Group newGroup)
    {
        Group.RemoveStudent(this);
        newGroup.AddStudent(this);
        Group = newGroup;
    }
}