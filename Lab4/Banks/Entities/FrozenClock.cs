using System.Timers;
using Banks.Abstractions;

namespace Banks.Entities;

public class FrozenClock : IClock
{
    public event TimeExpired? TimeHasBeenExpired;
    public event TimeExpired? MonthHasBeenPassed;
    public event TimeExpired? DayHasBeenPassed;
    public DateTime GetCurrentTime { get; } = DateTime.Now;

    public void TimeExpired(object? source, ElapsedEventArgs e)
    {
        TimeHasBeenExpired?.Invoke();
    }

    public void SetArbitraryTime(TimeSpan timeSpan)
    {
        var interval = TimeSpan.FromDays(1);
        for (DateTime curTime = GetCurrentTime; curTime < GetCurrentTime + timeSpan; curTime += TimeSpan.FromDays(1))
        {
            DayHasBeenPassed?.Invoke();
            interval += TimeSpan.FromDays(1);
            if (interval.TotalDays % 30 == 0)
            {
                MonthHasBeenPassed?.Invoke();
            }
        }
    }

    public void StartTimer()
    {
        Console.WriteLine("Ore wa Emilia ga suki da");
        Console.ReadLine();
    }
}