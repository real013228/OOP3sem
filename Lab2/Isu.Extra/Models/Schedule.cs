using Isu.Extra.DomainExceptions;
using Isu.Extra.Entities;

namespace Isu.Extra.Models;

public class Schedule
{
    private Schedule(List<Lesson> lessons)
    {
        Lessons = new List<Lesson>();
    }

    public static ScheduleBuilder Builder => new ScheduleBuilder();

    public IReadOnlyList<Lesson> Lessons { get; }

    public bool CheckIntersection(Schedule other)
    {
        return Lessons.All(lesson => other.Lessons.All(otherLesson => lesson.Begin != otherLesson.Begin));
    }

    public class ScheduleBuilder
    {
        private readonly List<Lesson> _lessons = new List<Lesson>();

        public void AddData(Lesson data)
        {
            _lessons.Add(data);
        }

        public Schedule Build()
        {
            return new Schedule(_lessons);
        }
    }
}