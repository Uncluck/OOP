using Backups.Exceptions;
using Backups.Interfaces;

namespace Backups.Models;

public class InMemoryRepository : IRepository
{
    public InMemoryRepository(string path)
    {
        if (string.IsNullOrEmpty(path)) throw new BackupException("Path is null");
        FullPath = path;
    }

    public RepositoryType Type => RepositoryType.MemoryType;
    public string FullPath { get; }

    public void CreateZip(string path, int number, BackupObject backupObject)
    {
    }

    public void ExtractZip(string path, string pathToFolder)
    {
    }
}