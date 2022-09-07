using Isu.Models;

namespace Isu.Entities;

public class Student
{
    private int? _id;
    private string? _name;
    private CourseNumber? _course;

    public Student()
    {
        _id = null;
        _name = string.Empty;
        _course = null;
    }

    public Student(int id, string name, CourseNumber course)
    {
        _id = id;
        _name = name;
        _course = course;
    }
}