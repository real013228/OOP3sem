using System.Diagnostics.SymbolStore;
using Backups.Abstractions;
using Backups.Models;
using Zio;

namespace Backups.Entities;

public class BackupObject : IEquatable<BackupObject>
{
    public BackupObject(string descriptor, IRepository repository)
    {
        Descriptor = descriptor;
        Repository = repository;
    }

    public string Descriptor { get; }
    public IRepository Repository { get; }

    public bool Equals(BackupObject? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Descriptor == other.Descriptor && Repository.Equals(other.Repository);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((BackupObject)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Descriptor, Repository);
    }
}