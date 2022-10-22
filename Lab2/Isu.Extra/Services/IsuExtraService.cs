using Isu.Entities;
using Isu.Extra.DomainExceptions;
using Isu.Extra.Entities;
using Stream = Isu.Extra.Entities.Stream;

namespace Isu.Extra.Services;

public class IsuExtraService : IIsuExtraService
{
    private readonly List<StudentExtra> _students = new List<StudentExtra>();
    private readonly List<GroupExtra> _groups = new List<GroupExtra>();
    private readonly List<ExtraStudy> _studies = new List<ExtraStudy>();
    public ExtraStudy CreateExtraStudy(string name, MegaFaculty megaFaculty)
    {
        ExtraStudy extraStudy = megaFaculty.CreateExtraStudy(name);
        _studies.Add(extraStudy);
        return extraStudy;
    }

    public void AddEnrollStudent(StudentExtra student, ExtraStudy extraStudy)
    {
        student.AddEnroll(extraStudy);
    }

    public void RemoveEnrollStudent(StudentExtra student, ExtraStudy extraStudy)
    {
        student.RemoveEnroll(extraStudy);
    }

    public IReadOnlyList<Stream> GetStreamsCourse(ExtraStudy course)
    {
        return course.Streams;
    }

    public IReadOnlyList<StudentExtra> GetStudentList(Stream extraStudyStream)
    {
        return extraStudyStream.Group;
    }

    public IEnumerable<StudentExtra> GetStudentListWithoutExtraStudy(GroupExtra group)
    {
        return group.GetStudentsWithoutExtraStudy();
    }

    public void SyncStudent(Student student, Group group)
    {
        _students.Add(new StudentExtra(student, new GroupExtra(group)));
    }

    public void SyncGroup(Group group)
    {
        _groups.Add(new GroupExtra(group));
    }

    public StudentExtra GetStudent(Student student)
    {
        if (_students.FirstOrDefault(x => x.Id == student.Id.Id) != null)
        {
            return _students.First(x => x.Id == student.Id.Id);
        }

        throw IsuExtraServiceExceptions.InvalidStudentId(student.Id.Id);
    }

    public bool ExtraStudyExists(ExtraStudy extraStudy)
    {
        return _studies.Any(study => extraStudy.Id == study.Id);
    }
}