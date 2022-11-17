﻿using System.Collections.ObjectModel;
using Backups.Abstractions;
using Backups.Algorithms;
using Backups.Models;

namespace Backups.Entities;

public class RestorePoint
{
    private readonly List<BackupObject> _backupObjects;

    public RestorePoint(List<BackupObject> backupObjects, IStorage storage)
    {
        _backupObjects = backupObjects;
        Storage = storage;
        CreationDate = DateTime.Now;
        Id = Guid.NewGuid();
    }

    public IReadOnlyCollection<BackupObject> BackupObjects => _backupObjects;
    public DateTime CreationDate { get; }
    public IStorage Storage { get; }
    public Guid Id { get; }
}