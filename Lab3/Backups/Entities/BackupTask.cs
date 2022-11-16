using System.Collections.ObjectModel;
using Backups.Abstractions;
using Backups.Algorithms;
using Backups.Models;

namespace Backups.Entities;

public class BackupTask : IBackupTask
{
    private readonly IRepository _repository;
    private readonly IStorageAlgorithm _algorithm;
    private readonly IArchiver _archiver;
    private readonly List<BackupObject> _objects;
    private readonly IBackup _backup;

    public BackupTask(IRepository repository, IStorageAlgorithm algorithm, IArchiver archiver, string name)
    {
        _objects = new List<BackupObject>();
        _backup = new Backup();
        _algorithm = algorithm;
        _archiver = archiver;
        Name = new MyPath(name);
        _repository = repository;
    }

    public MyPath Name { get; }
    public IEnumerable<RestorePoint> RestorePoints => _backup.RestorePoints;

    public void AddBackupObject(BackupObject obj)
    {
        if (_objects.Contains(obj))
        {
            throw new NullReferenceException();
        }

        _objects.Add(obj);
    }

    public void RemoveBackupObject(BackupObject obj)
    {
        if (!_objects.Contains(obj))
        {
            throw new NullReferenceException();
        }

        _objects.Remove(obj);
    }

    public RestorePoint DoJob()
    {
        var restorePoint = new RestorePoint(_objects);
        _backup.AddRestorePoint(restorePoint);
        _algorithm.CreateStorage(_objects, _repository, _archiver, _repository.CreateDirectory(Name.PathName));
        return restorePoint;
    }
}