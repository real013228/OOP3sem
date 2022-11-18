using Backups.Abstractions;
using Backups.Entities;
using Backups.Models;

namespace Backups.Algorithms;

public class SplitStorage<TArchiver> : IStorageAlgorithm
    where TArchiver : IArchiver
{
    private readonly TArchiver _archiver;
    private string _log;

    public SplitStorage(TArchiver archiver)
    {
        _archiver = archiver;
        _log = string.Empty;
    }

    public IStorage CreateStorage(IReadOnlyCollection<IRepoObject> objects, IRepository repository, string path)
    {
        var storages = new List<IStorage>();
        foreach (var obj in objects)
        {
            var repoObjs = new List<IRepoObject> { obj };
            IStorage storage = _archiver.DoArchive(repoObjs, repository, path);
            storages.Add(storage);
            _log = $"Split storage algorithm give objects away to archiver: {_archiver.ToString()}";
        }

        var adapter = new SplitStorageAdapter(storages, repository, _archiver, path);
        return adapter;
    }

    public override string ToString()
    {
        return _log;
    }
}