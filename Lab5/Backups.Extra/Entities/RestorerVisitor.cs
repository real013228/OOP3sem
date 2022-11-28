using Backups.Abstractions;
using Backups.Extra.Abstractions;
using Backups.Models;

namespace Backups.Extra.Entities;

public class RestorerVisitor : IArchiverVisitor
{
    private readonly Stack<string> _stack;
    private readonly IRepositoryExtra _repository;

    public RestorerVisitor(string path, IRepositoryExtra repository)
    {
        _repository = repository;
        _stack = new Stack<string>();
        _stack.Push(path);
    }

    public void Visit(IRepoFile obj)
    {
        string newFolder = _stack.Peek();
        _repository.CreateDirectory(newFolder);
        using Stream stream = _repository.OpenFileStream(MyPath.PathCombine(newFolder, obj.Name.PathName));
        obj.RepoObjStream().CopyTo(stream);
    }

    public void Visit(IRepoDirectory obj)
    {
        string newFolder = MyPath.PathCombine(_stack.Peek(), obj.Name.PathName);
        _stack.Push(newFolder);
        foreach (IRepoObject child in obj.Components())
        {
            child.Accept(this);
        }

        _stack.Pop();
    }
}