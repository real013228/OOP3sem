using Isu.Entities;
using Isu.Models;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private const int TabelNum = 100000;
    private List<Group> _groups = new List<Group>();
    private List<Student> _students = new List<Student>();
    public Group AddGroup(GroupName name)
    {
        var newGroup = new Group(name);
        _groups.Add(newGroup);
        return newGroup;
    }

    public Student AddStudent(Group group, string name)
    {
        var newStudent = new Student(TabelNum + StudentsCount(), name, group);
        _students.Add(newStudent);
        return newStudent;
    }

    public Student GetStudent(int id)
    {
        try
        {
            return _students[id - TabelNum];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Student? FindStudent(int id)
    {
        if (id - TabelNum < StudentsCount())
        {
            Student? student = _students[id - TabelNum];
            return student;
        }

        return null;
    }

    public List<Student> FindStudents(GroupName groupName)
    {
        IEnumerable<Student> students = from student in _students
            where student.Group.NameOfGroup.Equals(groupName)
            select student;
        return (List<Student>)students;
    }

    public List<Student> FindStudents(CourseNumber courseNumber)
    {
        var students = new List<Student>();
        foreach (Student student in _students)
        {
            if (student.Group.NameOfGroup.Course == courseNumber)
            {
                students.Add(student);
            }
        }

        return students;
    }

    public Group? FindGroup(GroupName groupName)
    {
        foreach (Group group in _groups)
        {
            if (group.NameOfGroup == groupName)
            {
                return group;
            }
        }

        return null;
    }

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        var groups = new List<Group>();
        foreach (Group group in _groups)
        {
            if (group.NameOfGroup.Course == courseNumber)
            {
                groups.Add(group);
            }
        }

        return groups;
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        foreach (var group in _groups)
        {
            if (group.NameOfGroup == student.Group.NameOfGroup)
            {
                var newStudent = new Student(student.Id, student.Name, newGroup);
                _students.Remove(student);
                _students.Add(newStudent);
            }
        }
    }

    private int StudentsCount()
    {
        return _students.Count;
    }
}