﻿using System.IO.Compression;
using Backups.Abstractions;
using Backups.Entities.ZipObjects;
using Backups.Models;
using ZipFile = Backups.Entities.ZipObjects.ZipFile;

namespace Backups.Entities;

public class ZipArchiverVisitor : IArchiverVisitor
{
    private readonly Stack<ZipArchive> _stack;
    private readonly Stack<List<IZipObject>> _otherStack;

    public ZipArchiverVisitor(ZipArchive archive)
    {
        _stack = new Stack<ZipArchive>();
        _stack.Push(archive);
        _otherStack = new Stack<List<IZipObject>>();
        _otherStack.Push(new List<IZipObject>());
    }

    public IEnumerable<IZipObject> Top => _otherStack.Peek();

    public void Visit(IRepoFile obj)
    {
        ZipArchiveEntry newZip = _stack.Peek().CreateEntry($@"{obj.Name.PathName}");
        Stream stream = newZip.Open();
        Stream stream2 = obj.RepoObjStream();
        stream2.CopyTo(stream);
        var zipFile = new ZipFile($@"{obj.Name.PathName}");
        _otherStack.Peek().Add(zipFile);
        stream2.Dispose();
        stream.Dispose();
    }

    public void Visit(IRepoDirectory obj)
    {
        Stream stream = _stack.Peek()
            .CreateEntry($@"{obj.Name.PathName}.zip").Open();
        using var newZip = new ZipArchive(stream, ZipArchiveMode.Create);
        _stack.Push(newZip);
        _otherStack.Push(new List<IZipObject>());
        foreach (IRepoObject child in obj.Components())
        {
            child.Accept(this);
        }

        var zipFolder = new ZipFolder(
            _otherStack.Pop(),
            Path.Combine($@"{obj.Name.PathName}"));
        _otherStack.Peek().Add(zipFolder);
        _stack.Pop();
    }
}