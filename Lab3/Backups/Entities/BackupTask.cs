using System.Collections.ObjectModel;
using System.Reflection.Metadata.Ecma335;
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

    public BackupTask(IBackup backup, IRepository repository, IStorageAlgorithm algorithm, IArchiver archiver, string name)
    {
        _objects = new List<BackupObject>();
        _backup = backup;
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
        var objects = _objects.Select(obj => _repository.GetRepoObject(new MyPath(obj.Descriptor))).ToList();
        var restorePoint = new RestorePoint(_objects, _algorithm.CreateStorage(objects, _repository, _archiver, _repository.CreateDirectory(MyPath.PathCombine(Name.PathName, $"{DateTime.Now:yyyy-dd-M--HH-mm-ss}"))));
        _backup.AddRestorePoint(restorePoint);
        return restorePoint;
    }
}