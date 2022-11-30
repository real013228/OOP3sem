namespace Banks.Abstractions;

public interface IClock
{
    DateTime GetCurrentTime { get; }
}