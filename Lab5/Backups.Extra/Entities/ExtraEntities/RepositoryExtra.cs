using Backups.Abstractions;
using Backups.Entities;
using Backups.Extra.Abstractions;
using Backups.Models;

namespace Backups.Extra.Entities.ExtraEntities;

public class RepositoryExtra : IRepositoryExtra
{
    private readonly IRepository _repository;

    public RepositoryExtra(MyPath path, IRepository repository)
    {
        Path = path;
        _repository = repository;
    }

    public MyPath Path { get; }

    public IRepoObject GetRepoObject(MyPath path)
    {
        return _repository.GetRepoObject(path);
    }

    public Stream OpenWrite(string path)
    {
        return _repository.OpenWrite(path);
    }

    public string CreateDirectory(string name)
    {
        return !Directory.Exists(name) ? _repository.CreateDirectory(name) : name;
    }

    public StreamWriter OpenFile(string path)
    {
        return new StreamWriter(path);
    }

    public Stream OpenFileStream(string path)
    {
        if (File.Exists(path))
            File.Delete(path);
        return File.Open(path, FileMode.Create, FileAccess.ReadWrite);
    }

    public void DeleteRestorePoint(RestorePoint restorePoint, string path)
    {
        var dirInfo = new DirectoryInfo(MyPath.PathCombine(path, restorePoint.Name));

        foreach (FileInfo f in dirInfo.GetFiles())
        {
            f.Delete();
        }

        Directory.Delete(MyPath.PathCombine(path, restorePoint.Name));
    }
}