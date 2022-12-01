namespace Banks.Models;

public class Balance
{
    private decimal _value;

    public Balance(decimal value)
    {
        _value = value;
    }

    public void IncreaseMoney(decimal value)
    {
        _value += value;
    }

    public void DecreaseMoney(decimal value)
    {
        if (_value < value)
            throw new NullReferenceException();
        _value -= value;
    }
}