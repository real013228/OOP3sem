using Isu.Extra.Models;

namespace Isu.Extra.DomainExceptions;

public class ScheduleException : Exception
{
    private ScheduleException(string msg)
        : base(msg) { }

    public static ScheduleException InvalidSchedule()
    {
        return new ScheduleException($"Invalid schedule: there are intersections with current schedule");
    }
}