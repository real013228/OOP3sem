using Backups.Abstractions;
using Backups.Entities;

namespace Backups.Extra.Abstractions;

public interface IRestoreTask
{
    void Restore(RestorePoint restorePoint);
}