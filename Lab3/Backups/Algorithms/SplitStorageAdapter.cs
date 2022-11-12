using Backups.Abstractions;
using Backups.Entities;
using Backups.Models;

namespace Backups.Algorithms;

public class SplitStorageAdapter : IStorage
{
    private readonly List<IStorage> _storages;
    private readonly IRepository _repository;
    private readonly IArchiver _archiver;

    public SplitStorageAdapter(List<IStorage> storages, IRepository repository, IArchiver archiver)
    {
        _storages = storages;
        _repository = repository;
        _archiver = archiver;
    }

    public IStorageLifeTime CreateStorageLifeTime()
    {
        var repoObjects = new List<IRepoObject>();
        foreach (var obj in _storages)
        {
            repoObjects.AddRange(obj.CreateStorageLifeTime().RepoObjects);
        }

        Stream stream = _repository.OpenWrite(Path.Combine($"{_repository.Path.PathName}", $"{DateTime.Now:yyyy-dd-M--HH-mm-ss}.zip"));
        return _archiver.DoArchive(repoObjects, stream, _repository).CreateStorageLifeTime();
    }
}