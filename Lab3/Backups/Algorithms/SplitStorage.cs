using Backups.Abstractions;
using Backups.Entities;
using Backups.Models;

namespace Backups.Algorithms;

public class SplitStorage : IStorageAlgorithm
{
    public IStorage CreateStorage(IReadOnlyCollection<IRepoObject> objects, IRepository repository, IArchiver archiver, string path)
    {
        var storages = new List<IStorage>();
        foreach (var obj in objects)
        {
            var repoObjs = new List<IRepoObject> { obj };
            storages.Add(archiver.DoArchive(repoObjs, repository, MyPath.PathCombine(
                path,
                $"{MyPath.GetFileName(obj.Name.PathName)}{DateTime.Now:yyyy-dd-M--HH-mm-ss}.zip")));
        }

        var adapter = new SplitStorageAdapter(storages, repository, archiver, path);
        return adapter;
    }
}