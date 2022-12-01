namespace Banks.Abstractions;

public delegate void DepositAccountExpired();
public interface IClock
{
    event DepositAccountExpired? OnExpired;
    DateTime GetCurrentTime { get; }
}