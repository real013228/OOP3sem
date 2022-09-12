using Isu.CustomExceptions;
using Isu.Entities;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Test;

public class IsuServiceTests
{
    private IsuService _service = new IsuService();
    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        var student = new Student(334805, "Natsuki Subaru", new Group(new GroupName("M32011")));
        var group = new Group(new GroupName("M32011"));
        Assert.Throws<StudentHasGroupException>(() => _service.AddStudent(group, student.Name));
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        var group = new Group(new GroupName("M32000"));
        _service.AddStudent(group, "A");
        _service.AddStudent(group, "B");
        _service.AddStudent(group, "C");
        Assert.Throws<ReachedMaxStudentsPerGroupException>(() => _service.AddStudent(group, "Exception?"));
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        Assert.Throws<InvalidGroupNameException>(() => new GroupName("REZERO"));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        var group = new Group(new GroupName("M32022"));
        var student = new Student(334334, "Emilia", new Group(new GroupName("M32211")));
        _service.ChangeStudentGroup(student, group);
        Assert.Equal(student.Group, group);
    }
}