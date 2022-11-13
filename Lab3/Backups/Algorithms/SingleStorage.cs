using System.IO.Compression;
using Backups.Abstractions;
using Backups.Entities;
using Backups.Models;

namespace Backups.Algorithms;

public class SingleStorage : IStorageAlgorithm
{
    public IStorage CreateStorage(IReadOnlyCollection<BackupObject> objects, IRepository repository, IArchiver archiver, string path)
    {
        var objs = objects.Select(obj => repository.GetRepoObject(obj.Descriptor)).ToList();
        return archiver.DoArchive(objs, repository, IRepository.PathCombine(path, $@"{DateTime.Now:yyyy-dd-M--HH-mm}.zip"));
    }
}