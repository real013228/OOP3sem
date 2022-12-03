using Backups.Entities;
using Backups.Extra.Abstractions;

namespace Backups.Extra.Entities.Cleaners.Selectors;

public class IntersectCleaner : ISelector
{
    private readonly IEnumerable<ISelector> _cleaners;

    public IntersectCleaner(IEnumerable<ISelector> cleaners)
    {
        _cleaners = cleaners;
    }

    public IEnumerable<RestorePoint> SelectRestorePoints(IEnumerable<RestorePoint> restorePoints)
    {
        IReadOnlyList<RestorePoint> result = restorePoints.ToList();
        IEnumerable<RestorePoint> restorePointsToDelete = _cleaners.Aggregate(result, (current, selector) => current.Intersect(selector.SelectRestorePoints(restorePoints)).ToList());
        return restorePointsToDelete.Count() == restorePoints.Count()
            ? throw new NullReferenceException()
            : restorePointsToDelete;
    }
}