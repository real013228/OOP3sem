using Backups.Abstractions;
using Backups.Algorithms;

namespace Backups.Extra.Entities.ExtraEntities;

public class SingleStorageAlgorithmExtra<TArchiver> : IStorageAlgorithm
    where TArchiver : IArchiver
{
    private readonly SingleStorage<TArchiver> _algorithm;

    public SingleStorageAlgorithmExtra(SingleStorage<TArchiver> algorithm)
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