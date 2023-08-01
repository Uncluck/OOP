using Backups.Exceptions;

namespace Backups.Models;

public class Storage
{
    public Storage(string path)
    {
        if (string.IsNullOrWhiteSpace(path)) throw new BackupException("Path is null");
        Path = path;
    }

    public string Path { get; }
}