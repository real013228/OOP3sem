using Isu.Entities;
using Isu.Extra.DomainExceptions;
using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Entities;

public class GroupExtra
{
    private readonly List<StudentExtra> _groupExtraStudents;
    private readonly Group _group;

    public GroupExtra(Group group)
    {
        _group = group;
        GroupName = new GroupNameExtra(group.Name);
        Schedule = Schedule.Builder.Build();
        _groupExtraStudents = new List<StudentExtra>();
        _groupExtraStudents = _group.Students.Select(x => new StudentExtra(x, this)).ToList();
    }

    public GroupNameExtra GroupName { get; }
    public Schedule Schedule { get; }
    public IReadOnlyList<StudentExtra> GroupExtraStudents => _groupExtraStudents;
    public IEnumerable<StudentExtra> GetStudentsWithoutExtraStudy()
    {
        return _groupExtraStudents.Where(student => student.WithoutExtraStudy());
    }

    public void RemoveStudentGroup(Student student)
    {
        _group.RemoveStudent(student);
        _groupExtraStudents.Remove(_groupExtraStudents.First(x => x.Id == student.Id.Id));
    }

    public void AddStudentGroup(Student student)
    {
        _group.AddStudent(student);
        _groupExtraStudents.Add(new StudentExtra(student, this));
    }
}