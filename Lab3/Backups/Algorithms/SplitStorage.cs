using Backups.Abstractions;
using Backups.Entities;
using Backups.Models;

namespace Backups.Algorithms;

public class SplitStorage : IStorageAlgorithm
{
    public IStorage CreateStorage(IReadOnlyCollection<BackupObject> objects, IRepository repository, IArchiver archiver, string path)
    {
        var storages = new List<IStorage>();
        foreach (BackupObject obj in objects)
        {
            var repoObjs = new List<IRepoObject> { repository.GetRepoObject(obj.Descriptor) };
            storages.Add(archiver.DoArchive(repoObjs, repository, MyPath.PathCombine(
                path,
                $"{MyPath.GetFileName(obj.Descriptor.PathName)}{DateTime.Now:yyyy-dd-M--HH-mm-ss}.zip")));
        }

        var adapter = new SplitStorageAdapter(storages, repository, archiver, path);
        return adapter;
    }
}