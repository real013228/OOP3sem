using System.IO.Compression;
using Backups.Models;

namespace Backups.Abstractions;

public interface IZipObject
{
    MyPath Name { get; }
    IRepoObject CreateRepoObject(ZipArchiveEntry zipArchiveEntry);
}