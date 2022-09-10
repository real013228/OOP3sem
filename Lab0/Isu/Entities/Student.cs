using Isu.Models;

namespace Isu.Entities;

public class Student
{
    public Student(int id, string name, Group group)
    {
        Id = id;
        Name = name;
        Group = group;
    }

    public string Name { get; }
    public int Id { get; }
    public Group Group { get; }
}