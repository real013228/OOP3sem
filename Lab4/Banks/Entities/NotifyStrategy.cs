using Banks.Abstractions;

namespace Banks.Entities;

public class NotifyStrategy : INotifyStrategy
{
    private Client _client;
    private Action<string> _func;

    public NotifyStrategy(Client client, Action<string> func)
    {
        _client = client;
        _func = func;
    }

    public void Notify(string msg)
    {
        _func(msg);
    }
}