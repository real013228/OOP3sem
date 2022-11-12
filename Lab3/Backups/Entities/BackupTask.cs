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
    private IBackup _backup;
    private Collection<BackupObject> _objects;

    public BackupTask(IRepository repository, IStorageAlgorithm algorithm, IArchiver archiver, string name)
    {
        _objects = new Collection<BackupObject>();
        _backup = new Backup();
        _algorithm = algorithm;
        _archiver = archiver;
        Name = new MyPath(name);
        _repository = repository;
    }

    public MyPath Name { get; }

    public void SetBackupObjects(Collection<BackupObject> objects)
    {
        _objects = objects;
    }

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
        restorePoint.DoSnapshot(_algorithm, _repository, _archiver);
        return restorePoint;
    }
}