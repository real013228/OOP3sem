using Backups.Entities;

namespace Backups.Abstractions;

public interface IBackup
{
    public IReadOnlyCollection<RestorePoint> RestorePoints { get; }
}