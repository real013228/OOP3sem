using Isu.CustomExceptions;
using Isu.Models;

namespace Isu.Entities;

public class Student
{
    public Student(int id, string name, Group group)
    {
        if (CorrectStudent(id))
        {
            Id = id;
            Name = name;
            Group = group;
        }
        else
        {
            throw new InvalidStudentIdException(id);
        }

        group.AddStudent(this);
    }

    public string Name { get; }
    public int Id { get; }

    public Group Group
    {
        get;
        private set;
    }

    public void ChangeGroup(Group newGroup)
    {
        Group = newGroup;
    }

    private bool CorrectStudent(int id)
    {
        return Enumerable.Range(100000, 999999).Contains(id);
    }
}