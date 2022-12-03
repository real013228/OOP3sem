using Backups.Entities;
using Backups.Extra.Abstractions;

namespace Backups.Extra.Entities.Cleaners.Selectors;

public class UnionCleaner : ISelector
{
    private readonly IEnumerable<ISelector> _cleaners;

    public UnionCleaner(IEnumerable<ISelector> cleaners)
    {
        _cleaners = cleaners;
    }

    public IEnumerable<RestorePoint> SelectRestorePoints(IEnumerable<RestorePoint> restorePoints)
    {
        IReadOnlyList<RestorePoint> result = new List<RestorePoint>();
        IEnumerable<RestorePoint> restorePointsToDelete = _cleaners.Aggregate(result, (current, selector) => current.Union(selector.SelectRestorePoints(restorePoints)).ToList());
        return restorePointsToDelete.Count() == restorePoints.Count()
            ? throw new NullReferenceException()
            : restorePointsToDelete;
    }
}