using Backups.Models;

namespace Backups.Abstractions;

public interface IRepoObject
{
    public MyPath Name { get; }
    public void Accept(IArchiverVisitor archiverVisitor);
}