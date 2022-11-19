namespace Backups.Abstractions;

public interface IDateTimeProvider
{
    DateTime GetTime();
}