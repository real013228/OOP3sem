using Backups.Abstractions;

namespace Backups.Entities;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetTime()
    {
        return DateTime.Now;
    }
}