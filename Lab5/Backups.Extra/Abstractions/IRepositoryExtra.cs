using Backups.Abstractions;
using Backups.Entities;

namespace Backups.Extra.Abstractions;

public interface IRepositoryExtra : IRepository
{
    StreamWriter OpenFile(string path);
    Stream OpenFileStream(string path);
    void DeleteRestorePoint(RestorePoint restorePoint, string path);
}