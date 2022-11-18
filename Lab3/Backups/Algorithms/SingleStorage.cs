using System.IO.Compression;
using Backups.Abstractions;
using Backups.Entities;
using Backups.Models;

namespace Backups.Algorithms;

public class SingleStorage<TArchiver> : IStorageAlgorithm
    where TArchiver : IArchiver
{
    private readonly TArchiver _archiver;
    private string _log;

    public SingleStorage(TArchiver archiver)
    {
        _archiver = archiver;
        _log = string.Empty;
    }

    public IStorage CreateStorage(IReadOnlyCollection<IRepoObject> objects, IRepository repository, string path)
    {
        IStorage storage = _archiver.DoArchive(objects, repository, path);
        _log = $"Single storage algorithm give objects away to archiver: {_archiver.ToString()}";
        return storage;
    }

    public override string ToString()
    {
        return _log;
    }
}