using Backups.Entities;

namespace Backups.Extra.Abstractions;

public interface ISelector
{
    IEnumerable<RestorePoint> SelectRestorePoints(IEnumerable<RestorePoint> restorePoints);
}