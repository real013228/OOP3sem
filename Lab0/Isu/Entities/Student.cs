using Isu.Models;

namespace Isu.Entities;

public class Student
{
    private int _id;
    private string _name;

    public Student(int id, string name, CourseNumber course, GroupName groupName)
    {
        _id = id;
        _name = name;
        Course = course;
        NameOfGroup = groupName;
    }

    public GroupName NameOfGroup { get; set; }

    public CourseNumber Course
    {
        get;
    }
}