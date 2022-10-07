using Isu.Models;

namespace Isu.Entities;

public class Student
{
    private int _id;
    private string _name;

    public Student(int id, string name, Group group)
    {
        _id = id;
        _name = name;
        Group = group;
    }

    public Group Group { get; }
}