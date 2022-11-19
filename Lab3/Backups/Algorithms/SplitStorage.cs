using Backups.Abstractions;
using Backups.Entities;
using Backups.Models;

namespace Backups.Algorithms;

public class SplitStorage<TArchiver> : IStorageAlgorithm
    where TArchiver : IArchiver
{
    private readonly TArchiver _archiver;

    public SplitStorage(TArchiver archiver)
    {
        _archiver = archiver;
    }

    public IStorage CreateStorage(IReadOnlyList<IRepoObject> objects, IRepository repository, string path)
    {
        var storages = objects.Select(obj => new List<IRepoObject> { obj }).Select(repoObjs => _archiver.DoArchive(repoObjs, repository, path)).ToList();

        var adapter = new SplitStorageAdapter(storages, repository, _archiver, path);
        return adapter;
    }
}