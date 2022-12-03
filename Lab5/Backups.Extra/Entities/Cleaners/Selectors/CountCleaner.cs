using Backups.Entities;
using Backups.Extra.Abstractions;

namespace Backups.Extra.Entities.Cleaners.Selectors;

public class CountCleaner : ISelector
{
    private readonly int _count;

    public CountCleaner(int count)
    {
        if (count == 0)
            throw new NullReferenceException();
        _count = count;
    }

    public IEnumerable<RestorePoint> SelectRestorePoints(IEnumerable<RestorePoint> restorePoints)
    {
        return restorePoints.Count() < _count ? Enumerable.Empty<RestorePoint>() : restorePoints.Take(restorePoints.Count() - _count);
    }
}