using Isu.CustomExceptions;
using Isu.Entities;
using Isu.Models;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private int _tabelNum = 0;
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
        var id = new Id();
        var newStudent = new Student(id.IdGenerator(_tabelNum++), name, group);
        _students.Add(newStudent);
        return newStudent;
    }

    public Student GetStudent(int id)
    {
        Student? student = FindStudent(id);
        if (student != null)
        {
            return student;
        }
        else
        {
            throw new CannotFindStudentWithIdException(id);
        }
    }

    public Student? FindStudent(int id)
    {
        Student? student = _students.First(x => x.Id == id);
        return student;
    }

    public List<Student> FindStudents(GroupName groupName)
    {
        return _students.Where(x => x.Group.NameOfGroup == groupName).ToList();
    }

    public List<Student> FindStudents(CourseNumber courseNumber)
    {
        return _students.Where(student => student.Group.NameOfGroup.Course == courseNumber).ToList();
    }

    public Group? FindGroup(GroupName groupName)
    {
        return _groups.FirstOrDefault(group => group.NameOfGroup == groupName);
    }

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        return _groups.Where(group => group.NameOfGroup.Course == courseNumber).ToList();
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        student.ChangeGroup(newGroup);
    }
}