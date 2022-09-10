using Isu.Entities;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Test;

public class IsuServiceTests
{
    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        var groupName = new GroupName("M32011");
        var group = new Group(groupName);
        var service = new IsuService();
        service.AddGroup(groupName);
        var expectedStudent = service.AddStudent(group, "Kirill Savvinov");
        Assert.Equal(expectedStudent.NameOfGroup, groupName);
        Assert.Contains(expectedStudent, group.Students);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        var groupName = new GroupName("M32011");
        var group = new Group(groupName);
        var service = new IsuService();
        for (int i = 0; i < 20; i++)
        {
            service.AddStudent(group, "Kirill Savvinov");
        }

        Assert.Throws<Isu.Services.ReachMaxStudentPerGroupException>(() => service.AddStudent(group, "Kirill Savvinov"));
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        var goodGroupName = new GroupName("M32011");
        Assert.Equal(goodGroupName, goodGroupName);
        Assert.Throws<CreateGroupWithInvalidNameException>(() => new GroupName("REZERO"));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        var course = new CourseNumber(1);
        var groupName = new GroupName("M32011");
        var anotherGroupName = new GroupName("M32001");
        var currentGroup = new Group(groupName);
        var anotherGroup = new Group(anotherGroupName);
        var student = new Student(334805, "Subaru Natsuki", course, groupName);
        var service = new IsuService();
        service.ChangeStudentGroup(student, anotherGroup);
        Assert.Equal(student.NameOfGroup, anotherGroupName);
        Assert.NotEqual(student.NameOfGroup, groupName);
    }
}