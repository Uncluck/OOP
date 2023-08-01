using Backups.Exceptions;

namespace Backups.Models;

public class BackupObject
{
    public BackupObject(string path, string name)
    {
        if (string.IsNullOrEmpty(path)) throw new BackupException("File's path is null");
        if (string.IsNullOrEmpty(name)) throw new BackupException("File's name is null");
        Path = path;
        Name = name;
    }

    public string Path { get; }
    public string Name { get; }
}