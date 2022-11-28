using Backups.Extra.Abstractions;
using Backups.Models;

namespace Backups.Extra.Entities.Loggers;

public class FileLogger : ILogger
{
    private readonly string _path;
    private readonly IRepositoryExtra _repositoryExtra;
    private readonly bool _withTimeCode;
    private readonly string _name;

    public FileLogger(string path, IRepositoryExtra repositoryExtra, bool withTimeCode)
    {
        _path = MyPath.PathCombine(path, $"log-{Guid.NewGuid()}");
        _repositoryExtra = repositoryExtra;
        _withTimeCode = withTimeCode;
        _repositoryExtra.CreateDirectory(_path);
        _name = $"log-{Guid.NewGuid()}.txt";
    }

    public void Log(string log)
    {
        if (_withTimeCode)
            log = $"{DateTime.Now}\n{log}";
        using StreamWriter stream = _repositoryExtra.OpenFile(MyPath.PathCombine(_path, _name));
        stream.Write(log);
    }
}