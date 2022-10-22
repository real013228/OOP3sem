using Isu.Entities;
using Isu.Extra.DomainExceptions;
using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Extra.Services;
using Isu.Models;
using Isu.Services;
using Xunit;
using Stream = Isu.Extra.Entities.Stream;

namespace Isu.Extra.Test.Tests;

public class IsuExtraServiceTests
{
    private readonly IsuExtraService _isuExtra = new IsuExtraService();
    private readonly IsuService _isu = new IsuService();

    [Fact]
    public void AddNewExtraStudy_ExtraStudyExists()
    {
        var megaFaculty = new MegaFaculty(new GroupName("M32011"));
        ExtraStudy extraStudy = _isuExtra.CreateExtraStudy("physics", megaFaculty);
        Assert.True(_isuExtra.ExtraStudyExists(extraStudy));
    }

    [Fact]
    public void StudentEnrollToExtraStudy_HasExtraStudy()
    {
        var oldGroup = _isu.AddGroup(new GroupName("M32011"));
        var megaFaculty = new MegaFaculty(new GroupName("U32011"));
        var oldStudent = _isu.AddStudent(oldGroup, "Cyril Savvinov");
        var group = new GroupExtra(oldGroup);
        var student = new StudentExtra(oldStudent, group);
        var extraStudy = _isuExtra.CreateExtraStudy("PHYSICS", megaFaculty);
        _isuExtra.AddEnrollStudent(student, extraStudy);
        Assert.Contains(extraStudy, student.Study);
    }

    [Fact]
    public void RemoveEnrollingFromStudent_StudentHasNotAnyExtraStudy()
    {
        var oldGroup = _isu.AddGroup(new GroupName("M32011"));
        var megaFaculty = new MegaFaculty(new GroupName("U32011"));
        var oldStudent = _isu.AddStudent(oldGroup, "Cyril Savvinov");
        var group = new GroupExtra(oldGroup);
        var student = new StudentExtra(oldStudent, group);
        var extraStudy = _isuExtra.CreateExtraStudy("PHYSICS", megaFaculty);
        _isuExtra.AddEnrollStudent(student, extraStudy);
        _isuExtra.RemoveEnrollStudent(student, extraStudy);
        Assert.Equal(0, student.Study.Count);
    }

    [Fact]
    public void CreateTwoStreamsInExtraStudy_ExtraStudyContainsTwoStreams()
    {
        var oldGroup = _isu.AddGroup(new GroupName("M32011"));
        var megaFaculty = new MegaFaculty(new GroupName("U32011"));
        var oldStudent = _isu.AddStudent(oldGroup, "Cyril Savvinov");
        var group = new GroupExtra(oldGroup);
        var student = new StudentExtra(oldStudent, group);
        var extraStudy = _isuExtra.CreateExtraStudy("PHYSICS", megaFaculty);
        extraStudy.AddStream();
        extraStudy.AddStream();
        Assert.Equal(2, _isuExtra.GetStreamsCourse(extraStudy).Count);
    }

    [Fact]
    public void CreateStudent_StudentContainedInExtraStudy()
    {
        var oldGroup = _isu.AddGroup(new GroupName("M32011"));
        var megaFaculty = new MegaFaculty(new GroupName("U32011"));
        var oldStudent = _isu.AddStudent(oldGroup, "Cyril Savvinov");
        var group = new GroupExtra(oldGroup);
        var student = new StudentExtra(oldStudent, group);
        var extraStudy = _isuExtra.CreateExtraStudy("PHYSICS", megaFaculty);
        var stream = extraStudy.AddStream();
        _isuExtra.AddEnrollStudent(student, extraStudy);
        Assert.Contains(student, _isuExtra.GetStudentList(stream));
    }

    [Fact]
    public void GetStudentWithoutExtraStudy()
    {
        var oldGroup = _isu.AddGroup(new GroupName("M32011"));
        var megaFaculty = new MegaFaculty(new GroupName("U32011"));
        var oldStudent1 = _isu.AddStudent(oldGroup, "Cyril Savvinov");
        var oldStudent2 = _isu.AddStudent(oldGroup, "Cyril Savvinov");
        var group = new GroupExtra(oldGroup);
        var extraStudy = _isuExtra.CreateExtraStudy("PHYSICS", megaFaculty);
        _isuExtra.AddEnrollStudent(group.GroupExtraStudents[0], extraStudy);
        Assert.Contains(group.GroupExtraStudents[1], _isuExtra.GetStudentListWithoutExtraStudy(group));
    }
}