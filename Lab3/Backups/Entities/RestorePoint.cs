using System.Collections.ObjectModel;
using Backups.Abstractions;
using Backups.Algorithms;
using Backups.Models;

namespace Backups.Entities;

public class RestorePoint
{
    private readonly Collection<BackupObject> _backupObjects;

    public RestorePoint(Collection<BackupObject> backupObjects)
    {
        _backupObjects = backupObjects;
        CreationDate = DateTime.Now;
    }

    public IReadOnlyCollection<BackupObject> BackupObjects => _backupObjects;
    public DateTime CreationDate { get; }

    public IStorage DoSnapshot(IStorageAlgorithm algorithm, IRepository repository, IArchiver archiver)
    {
        return algorithm.CreateStorage(_backupObjects, repository, archiver);
    }
}