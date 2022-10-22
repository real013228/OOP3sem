using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Models;
using Isu.Services;

namespace Isu.Extra.Services;

public class IsuServiceDecorator : IIsuService
{
    private readonly IIsuService _isuService = new IsuService();
    private readonly IIsuExtraService _isuExtraService = new IsuExtraService();
    private readonly IdGenerator _generator = new IdGenerator();

    public Group AddGroup(GroupName name)
    {
        _isuExtraService.SyncGroup(new Group(name));
        return _isuService.AddGroup(name);
    }

    public Student AddStudent(Group group, string name)
    {
        _isuExtraService.SyncStudent(new Student(_generator.GetNextId(), name, group), group);
        return _isuService.AddStudent(group, name);
    }

    public Student GetStudent(int id) => _isuService.GetStudent(id);

    public Student? FindStudent(int id) => _isuService.FindStudent(id);

    public List<Student> FindStudents(GroupName groupName) => _isuService.FindStudents(groupName);

    public List<Student> FindStudents(CourseNumber courseNumber) => _isuService.FindStudents(courseNumber);

    public Group? FindGroup(GroupName groupName)
    {
        return _isuService.FindGroup(groupName);
    }

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        return _isuService.FindGroups(courseNumber);
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        _isuService.ChangeStudentGroup(student, newGroup);
    }
}