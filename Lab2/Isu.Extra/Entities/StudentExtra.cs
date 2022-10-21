using Isu.Entities;
using Isu.Extra.DomainExceptions;
using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Entities;

public class StudentExtra
{
    private readonly List<ExtraStudy> _studies;
    private readonly Student _student;
    private GroupExtra _groupExtra;

    public StudentExtra(Student student, GroupExtra groupExtra)
    {
        _student = student;
        _groupExtra = groupExtra;
        _studies = new List<ExtraStudy>();
    }

    public IReadOnlyList<ExtraStudy> Study => _studies;
    public int Id => _student.Id.Id;
    public string Name => _student.Name;

    public void AddEnroll(ExtraStudy extraStudy)
    {
        if (Study.Count > 2)
        {
            throw StudentExtraException.InvalidEnrollReachedMax();
        }

        if (extraStudy.MegaFaculty == _groupExtra.GroupName.MegaFaculty)
        {
            throw StudentExtraException.InvalidExtraStudy(extraStudy.MegaFaculty);
        }

        if (extraStudy.Streams.Count != 0 &&
            extraStudy.Streams.All(stream => !stream.Schedule.CheckIntersection(_groupExtra.Schedule)))
        {
            throw StudentExtraException.InvalidExtraStudy();
        }

        Stream? stream;
        stream = extraStudy.Streams.Count == 0 ? extraStudy.AddStream() : extraStudy.GetStream();
        _studies.Add(extraStudy);
        extraStudy.Streams.First(x => x.Schedule.CheckIntersection(_groupExtra.Schedule)).AddStudent(this);
        stream.AddStudent(this);
    }

    public void RemoveEnroll(ExtraStudy study)
    {
        if (!Study.Contains(study))
        {
            throw StudentExtraException.InvalidRemovingEnrollToExtraStudy(study);
        }

        _studies.Remove(study);
    }

    public bool WithoutExtraStudy()
    {
        return _studies.Count == 0;
    }
}