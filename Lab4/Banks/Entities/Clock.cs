using System.Security.Cryptography;
using System.Timers;
using Banks.Abstractions;
using Timer = System.Timers.Timer;

namespace Banks.Entities;

public class Clock : IClock
{
    private static System.Timers.Timer _timerForArbitraryTime = new Timer();
    private static System.Timers.Timer _timerForDayChanged = new Timer();
    private static System.Timers.Timer _timerForMonthPassed = new Timer();
    private readonly double _accelerationFactor;

    public Clock(double accelerationFactor)
    {
        _accelerationFactor = accelerationFactor;
    }

    public Clock()
    {
        _accelerationFactor = 1;
    }

    public event TimeExpired? TimeHasBeenExpired;
    public event TimeExpired? MonthHasBeenPassed;
    public event TimeExpired? DayHasBeenPassed;
    public DateTime GetCurrentTime { get; }

    public void TimeExpired(object? source, ElapsedEventArgs e)
    {
        Console.WriteLine("Has been invoked");
        TimeHasBeenExpired?.Invoke();
    }

    public void DayExpired(object? source, ElapsedEventArgs e)
    {
        DayHasBeenPassed?.Invoke();
    }

    public void MonthExpired(object? source, ElapsedEventArgs e)
    {
        MonthHasBeenPassed?.Invoke();
    }

    public void SetArbitraryTime(TimeSpan timeSpan)
    {
        _timerForArbitraryTime = new Timer();
        _timerForArbitraryTime.Interval = timeSpan.TotalMilliseconds / _accelerationFactor;
        _timerForArbitraryTime.Elapsed += TimeExpired;
        _timerForArbitraryTime.Enabled = true;
    }

    public void SetDayChangedTime()
    {
        _timerForDayChanged = new Timer();
        _timerForDayChanged.Interval = TimeSpan.FromDays(1).TotalMilliseconds / _accelerationFactor;
        _timerForDayChanged.Elapsed += DayExpired;
        _timerForDayChanged.Enabled = true;
    }

    public void SetMonthChangedTime()
    {
        _timerForMonthPassed = new Timer();
        _timerForMonthPassed.Interval = TimeSpan.FromDays(30).TotalMilliseconds / _accelerationFactor;
        _timerForMonthPassed.Elapsed += MonthExpired;
        _timerForMonthPassed.Enabled = true;
    }

    public void StartTimer()
    {
        Console.WriteLine("Ore wa Emilia ga suki da");
        Console.ReadLine();
    }
}