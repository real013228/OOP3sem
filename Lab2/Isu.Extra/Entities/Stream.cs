using Isu.Entities;
using Isu.Extra.DomainExceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class Stream
{
    private const int MaxStudentsCount = 20;
    private readonly List<StudentExtra> _group;
    private readonly ExtraStudy _extraStudy;

    public Stream(Schedule schedule, ExtraStudy extraStudy)
    {
        Schedule = schedule;
        _extraStudy = extraStudy;
        _group = new List<StudentExtra>();
    }

    public Schedule Schedule { get; }
    public IReadOnlyList<StudentExtra> Group => _group;

    public void AddStudent(StudentExtra student)
    {
        if (!student.Study.Contains(_extraStudy))
        {
            throw StreamException.InvalidStudent(student);
        }

        _group.Add(student);
    }

    public void RemoveStudent(StudentExtra student)
    {
        if (!_group.Contains(student))
        {
            throw StreamException.InvalidRequest(student);
        }

        _group.Remove(student);
    }

    public bool StreamFull()
    {
        return Group.Count < MaxStudentsCount;
    }
}