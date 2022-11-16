using System.Diagnostics.SymbolStore;
using Backups.Abstractions;
using Backups.Models;
using Zio;

namespace Backups.Entities;

public class BackupObject
{
    public BackupObject(string descriptor, IRepository repository)
    {
        Descriptor = descriptor;
        Repository = repository;
    }

    public string Descriptor { get; }
    public IRepository Repository { get; }
}