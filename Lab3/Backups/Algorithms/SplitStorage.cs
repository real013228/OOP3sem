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

    public IStorage CreateStorage(IReadOnlyCollection<IRepoObject> objects, IRepository repository, string path)
    {
        var storages = new List<IStorage>();
        foreach (var obj in objects)
        {
            var repoObjs = new List<IRepoObject> { obj };
            storages.Add(_archiver.DoArchive(repoObjs, repository, MyPath.PathCombine(
                path,
                $"{MyPath.GetFileName(obj.Name.PathName)}{DateTime.Now:yyyy-dd-M--HH-mm-ss}.zip")));
        }

        var adapter = new SplitStorageAdapter(storages, repository, _archiver, path);
        return adapter;
    }
}