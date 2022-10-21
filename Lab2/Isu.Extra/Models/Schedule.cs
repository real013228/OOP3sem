using Isu.Extra.DomainExceptions;
using Isu.Extra.Entities;

namespace Isu.Extra.Models;

public class Schedule
{
    private readonly List<Lesson> _lessons;

    public Schedule()
    {
        _lessons = new List<Lesson>();
    }

    public IReadOnlyList<Lesson> Lessons => _lessons;

    public void AddLesson(Schedule schedule)
    {
        if (!CheckIntersection(schedule))
        {
            throw ScheduleException.InvalidSchedule();
        }

        foreach (Lesson lesson in schedule.Lessons)
        {
            _lessons.Add(lesson);
        }
    }

    public bool CheckIntersection(Schedule other)
    {
        return _lessons.All(lesson => other._lessons.All(otherLesson => lesson.Begin != otherLesson.Begin));
    }
}