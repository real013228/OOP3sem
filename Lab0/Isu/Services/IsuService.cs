using System.Collections.Generic;
using System.Linq;
using Isu.CustomExceptions;
using Isu.Entities;
using Isu.Models;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private readonly List<Group> _groups = new List<Group>();
    private readonly List<Student> _students = new List<Student>();
    private readonly IdGenerator _generator = new IdGenerator();

    public Group AddGroup(GroupName name)
    {
        if (_groups.Any(x => x.Name == name))
        {
            throw IsuServiceException.GroupAlreadyExists(name);
        }

        var newGroup = new Group(name);
        _groups.Add(newGroup);
        return newGroup;
    }

    public Student AddStudent(Group group, string name)
    {
        var newStudent = new Student(_generator.GetNextId(), name, group);
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

        throw IsuServiceException.CannotFindStudent(id);
    }

    public Student? FindStudent(int id)
    {
        var curId = new StudentId(id);
        Student? student = _students.FirstOrDefault(x => x.Id.Id == curId.Id);
        return student;
    }

    public List<Student> FindStudents(GroupName groupName)
    {
        return _students.Where(x => x.Group.Name.Name == groupName.Name).ToList();
    }

    public List<Student> FindStudents(CourseNumber courseNumber)
    {
        return _students.Where(student => student.Group.Name.Course.Number == courseNumber.Number).ToList();
    }

    public Group? FindGroup(GroupName groupName)
    {
        return _groups.FirstOrDefault(group => group.Name.Name == groupName.Name);
    }

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        return _groups.Where(group => group.Name.Course.Number == courseNumber.Number).ToList();
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        student.ChangeGroup(newGroup);
    }
}