using System.Diagnostics;
using Banks.Abstractions;
using Banks.Entities;
using Xunit;

namespace Banks.Test;

public class Test
{
    [Fact]
    public void ClockTest()
    {
        IClock clock = new Clock();
        clock.SetArbitraryTime(new TimeSpan(2000));
    }

    private static void OnTimedEvent(object? source, System.Timers.ElapsedEventArgs e)
    {
        Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
    }
}