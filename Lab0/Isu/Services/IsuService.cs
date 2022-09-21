using System.Collections.Generic;
using System.Linq;
using Isu.CustomExceptions;
using Isu.Entities;
using Isu.Models;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private List<Group> _groups = new List<Group>();
    private List<Student> _students = new List<Student>();
    private int _tableNum = 0;

    public Group AddGroup(GroupName name)
    {
        var newGroup = new Group(name);
        if (_groups.Contains(newGroup))
        {
            throw new GroupAlreadyExistsException(newGroup);
        }

        _groups.Add(newGroup);
        return newGroup;
    }

    public Student AddStudent(Group group, string name)
    {
        var id = new StudentId(_tableNum++);
        var newStudent = new Student(id, name, group);
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
        var curId = new StudentId(id);
        Student? student = _students.First(x => x.Id == curId);
        return student;
    }

    public List<Student> FindStudents(GroupName groupName)
    {
        return _students.Where(x => x.Group.Name == groupName).ToList();
    }

    public List<Student> FindStudents(CourseNumber courseNumber)
    {
        return _students.Where(student => student.Group.Name.Course == courseNumber).ToList();
    }

    public Group? FindGroup(GroupName groupName)
    {
        return _groups.FirstOrDefault(group => group.Name == groupName);
    }

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        return _groups.Where(group => group.Name.Course == courseNumber).ToList();
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        student.ChangeGroup(newGroup);
    }
}