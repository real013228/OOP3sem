using Backups.Extra.Abstractions;

namespace Backups.Extra.Entities.Loggers;

public class ConsoleLogger : ILogger
{
    private readonly bool _withTimeCode;

    public ConsoleLogger(bool withTimeCode)
    {
        _withTimeCode = withTimeCode;
    }

    public void Log(string log)
    {
        if (_withTimeCode)
            log = $"{DateTime.Now}\n{log}";
        Console.Write(log);
    }
}