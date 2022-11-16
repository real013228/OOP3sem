namespace Backups.Abstractions;

public interface IVisitor
{
    void Visit(IRepoFile obj);
    void Visit(IRepoDirectory obj);
}