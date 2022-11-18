using System.Collections.ObjectModel;
using System.Security.Cryptography;
using Backups.Abstractions;
using Backups.Algorithms;
using Backups.Models;

namespace Backups.Entities;

public class RestorePoint
{
    private readonly List<BackupObject> _backupObjects;

    public RestorePoint(List<BackupObject> backupObjects, IStorage storage, DateTime time, string name)
    {
        _backupObjects = backupObjects;
        Storage = storage;
        CreationDate = time;
        Name = name;
    }

    public IReadOnlyCollection<BackupObject> BackupObjects => _backupObjects;
    public DateTime CreationDate { get; }
    public IStorage Storage { get; }
    public string Name { get; }
}