using System.Globalization;
using Isu.Entities;
using Isu.Extra.DomainExceptions;
using Isu.Extra.Entities;
using Isu.Extra.Models;
using Stream = Isu.Extra.Entities.Stream;

namespace Isu.Extra.Services;

public interface IIsuExtraService
{
    public ExtraStudy CreateExtraStudy(string name, MegaFaculty megaFaculty);
    public void AddEnrollStudent(StudentExtra student, ExtraStudy extraStudy);
    public void RemoveEnrollStudent(StudentExtra student, ExtraStudy extraStudy);
    public IReadOnlyList<Stream> GetStreamsCourse(ExtraStudy course);
    public IReadOnlyList<StudentExtra> GetStudentList(Stream extraStudyStream);
    public IEnumerable<StudentExtra> GetStudentListWithoutExtraStudy(GroupExtra group);
    public void SyncStudent(Student student, Group group);
    public void SyncGroup(Group group);
    public StudentExtra GetStudent(Student student);
}