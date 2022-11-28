using Backups.Extra.Abstractions;

namespace Backups.Extra.Entities.Loggers;

public class FileLogger : ILogger
{
    private readonly string _path;
    private readonly IRepositoryExtra _repositoryExtra;
    private readonly bool _withTimeCode;

    public FileLogger(string path, IRepositoryExtra repositoryExtra, bool withTimeCode)
    {
        _path = path;
        _repositoryExtra = repositoryExtra;
        _withTimeCode = withTimeCode;
    }

    public void Log(string log)
    {
        if (_withTimeCode)
            log = $"{DateTime.Now}\n{log}";
        StreamWriter stream = _repositoryExtra.OpenFile(_path);
        stream.WriteLine(log);
    }
}