using Backups.Abstractions;
using Backups.Algorithms;

namespace Backups.Extra.Entities.ExtraEntities;

public class SplitStorageAlgorithmExtra<TArchiver> : IStorageAlgorithm
    where TArchiver : IArchiver
{
    private readonly SplitStorage<TArchiver> _algorithm;

    public SplitStorageAlgorithmExtra(SplitStorage<TArchiver> algorithm)
    {
        _algorithm = algorithm;
    }

    public IStorage CreateStorage(IReadOnlyList<IRepoObject> objects, IRepository repository, string path)
    {
        return _algorithm.CreateStorage(objects, repository, path);
    }

    public override string ToString()
    {
        return _algorithm.ToString() ?? throw new InvalidOperationException();
    }
}