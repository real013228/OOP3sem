namespace Backups.Abstractions;

public interface IArchiverVisitor
{
    void Visit(IRepoFile obj);
    void Visit(IRepoDirectory obj);
}