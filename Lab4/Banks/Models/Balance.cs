namespace Banks.Models;

public class Balance
{
    public Balance(decimal value)
    {
        Value = value;
    }

    public decimal Value { get; private set; }

    public decimal IncreaseMoney(decimal value)
    {
        Value += value;
        return value;
    }

    public decimal DecreaseMoney(decimal value)
    {
        if (Value < value)
            throw new NullReferenceException();
        Value -= value;
        return value;
    }
}