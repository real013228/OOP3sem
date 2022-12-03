using System.Timers;

namespace Banks.Abstractions;

public delegate void DayChanged();

public delegate void TimeExpired();

public interface IClock
{
    event TimeExpired? TimeHasBeenExpired;
    event TimeExpired? MonthHasBeenPassed;
    event TimeExpired? DayHasBeenPassed;
    DateTime GetCurrentTime { get; }
    public void SetArbitraryTime(TimeSpan timeSpan);
    void StartTimer();
}