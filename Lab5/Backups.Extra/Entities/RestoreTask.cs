using Backups.Abstractions;
using Backups.Entities;
using Backups.Extra.Abstractions;

namespace Backups.Extra.Entities;

public class RestoreTask : IRestoreTask
{
    private readonly IRestorer _restorer;

    public RestoreTask(IRestorer restorer)
    {
        _restorer = restorer;
    }

    public void Restore(RestorePoint restorePoint)
    {
        _restorer.Restore(restorePoint);
    }
}