using System.Collections.ObjectModel;
using Backups.Abstractions;
using Backups.Entities.ZipObjects;
using Backups.Models;

namespace Backups.Entities;

public class ZipStorage : IStorage
{
    private readonly IRepository _repository;
    public ZipStorage(MyPath path, ZipFolder folder, IRepository repository)
    {
        Path = path;
        Storages = folder;
        _repository = repository;
    }

    public MyPath Path { get; }

    public ZipFolder Storages { get; }
    public IRepository Repository => _repository;

    public IStorageLifeTime CreateStorageLifeTime()
    {
        return new StorageLifeTime(GetRepoObjects());
    }

    private IReadOnlyList<IRepoObject> GetRepoObjects()
    {
        return Storages.Children.Select(obj => _repository.GetRepoObject(obj.Name)).ToList();
    }
}