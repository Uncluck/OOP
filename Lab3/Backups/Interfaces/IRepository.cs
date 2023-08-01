using System.IO.Compression;
using Backups.Models;

namespace Backups.Interfaces;

public interface IRepository
{
    RepositoryType Type { get; }
    string FullPath { get; }
    void CreateZip(string path, int number, BackupObject backupObject);
    void ExtractZip(string path, string pathToFolder);
}