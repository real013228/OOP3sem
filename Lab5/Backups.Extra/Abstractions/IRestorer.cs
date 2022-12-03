using Backups.Abstractions;
using Backups.Entities;

namespace Backups.Extra.Abstractions;

public interface IRestorer
{
    void Restore(RestorePoint restorePoint);
}