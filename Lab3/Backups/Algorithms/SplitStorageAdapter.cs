using Backups.Abstractions;
using Backups.Entities;
using Backups.Models;

namespace Backups.Algorithms;

public class SplitStorageAdapter : IStorage
{
    private readonly List<IStorage> _storages;
    private readonly IRepository _repository;
    private readonly IArchiver _archiver;
    private readonly string _path;

    public SplitStorageAdapter(List<IStorage> storages, IRepository repository, IArchiver archiver, string path)
    {
        _storages = storages;
        _repository = repository;
        _archiver = archiver;
        _path = path;
    }

    public IStorageLifeTime CreateStorageLifeTime()
    {
        var repoObjects = new List<IRepoObject>();
        foreach (var obj in _storages)
        {
            repoObjects.AddRange(obj.CreateStorageLifeTime().RepoObjects);
        }

        return _archiver.DoArchive(repoObjects, _repository, IRepository.PathCombine(_path, $"{DateTime.Now:yyyy-dd-M--HH-mm-ss}.zip")).CreateStorageLifeTime();
    }
}