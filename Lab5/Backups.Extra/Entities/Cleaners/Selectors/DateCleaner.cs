using Backups.Entities;
using Backups.Extra.Abstractions;

namespace Backups.Extra.Entities.Cleaners.Selectors;

public class DateCleaner : ISelector
{
    private readonly DateTime _time;

    public DateCleaner(DateTime time)
    {
        _time = time;
    }

    public IEnumerable<RestorePoint> SelectRestorePoints(IEnumerable<RestorePoint> restorePoints)
    {
        IEnumerable<RestorePoint> restorePointsToDelete = restorePoints.Where(rp => rp.CreationDate < _time);
        return restorePointsToDelete.Count() == restorePoints.Count()
            ? throw new NullReferenceException()
            : restorePointsToDelete;
    }
}