namespace Banks.Abstractions;

public delegate void DayChanged();
public interface IClock
{
    DateTime GetCurrentTime { get; }
}