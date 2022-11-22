using Backups.Abstractions;
using Backups.Models;

namespace Backups.Entities;

public class BackupTask : IBackupTask
{
    private readonly IRepository _repository;
    private readonly IStorageAlgorithm _algorithm;
    private readonly List<BackupObject> _objects;
    private readonly IBackup _backup;
    private readonly IDateTimeProvider _time;
    public BackupTask(IBackup backup, IRepository repository, IStorageAlgorithm algorithm, string name, IDateTimeProvider time)
    {
        _objects = new List<BackupObject>();
        _backup = backup;
        _algorithm = algorithm;
        _time = time;
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

        _objects.Add(new BackupObject(obj.Descriptor, _repository));
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
        string restorePointName = $"{DateTime.Now:yyyy-dd-M--HH-mm-ss}";
        string pathName = MyPath.PathCombine(Name.PathName, restorePointName);
        string path = _repository.CreateDirectory(pathName);
        IStorage storage = _algorithm.CreateStorage(objects, _repository, path);
        var restorePoint = new RestorePoint(new List<BackupObject>(_objects), storage, _time.GetTime(), restorePointName);
        _backup.AddRestorePoint(restorePoint);
        return restorePoint;
    }
}