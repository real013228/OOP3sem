using Backups.Abstractions;
using Backups.Entities;

namespace Backups.Algorithms;

public class SplitStorage : IStorageAlgorithm
{
    public IStorage CreateStorage(IReadOnlyCollection<BackupObject> objects, IRepository repository, IArchiver archiver)
    {
        var storages = new List<IStorage>();
        foreach (BackupObject obj in objects)
        {
            var repoObjs = new List<IRepoObject> { repository.GetRepoObject(obj.Descriptor) };
            Stream stream =
                repository.OpenWrite(
                    $"{repository.Path.PathName}{DateTime.Now:yyyy-dd-M--HH-mm-ss}.zip");
            storages.Add(archiver.DoArchive(repoObjs, stream, repository));
        }

        var adapter = new SplitStorageAdapter(storages, repository, archiver);
        return adapter;
    }
}