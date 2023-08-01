using System.IO.Compression;
using Backups.Exceptions;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Entities;

public class Repository : IRepository
{
    public Repository(string path)
    {
        if (string.IsNullOrEmpty(path)) throw new BackupException("Path is null");
        FullPath = path;
    }

    public RepositoryType Type => RepositoryType.FileType;
    public string FullPath { get; }

    public void CreateZip(string path, int number, BackupObject backupObject)
    {
        if (File.Exists(path))
        {
            using ZipArchive zipArchiveVariable = ZipFile.Open(path, ZipArchiveMode.Update);
            zipArchiveVariable.CreateEntryFromFile(Path.Combine(backupObject.Path, backupObject.Name), backupObject.Name);
            zipArchiveVariable.Dispose();
        }
        else
        {
            using ZipArchive zipArchiveVariable = ZipFile.Open(path, ZipArchiveMode.Create);
            zipArchiveVariable.CreateEntryFromFile(Path.Combine(backupObject.Path, backupObject.Name), $"{backupObject.Name}_{number}.{Path.GetExtension(backupObject.Name)}");
            zipArchiveVariable.Dispose();
        }
    }

    public void ExtractZip(string path, string pathToFolder)
    {
        using ZipArchive zipArchiveVariable = ZipFile.Open(path, ZipArchiveMode.Update);
        zipArchiveVariable.ExtractToDirectory(pathToFolder);
    }
}