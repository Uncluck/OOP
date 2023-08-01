using System.IO.Compression;
using Backups.Entities;
using Backups.Extra.Interfaces;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Extra.Algorithms;

public class OriginRestoreToVersion : IRestoreToVersion
{
    public void Execute(Backup backup, int version, Configurator configurator)
    {
        var point = backup.RestorePoints.Single(point => point.Number == version);
        string pathToDirectory = Path.Combine(Directory.GetCurrentDirectory(), "tmp");
        foreach (Storage pointStorage in point.Storages)
        {
            Directory.CreateDirectory(pathToDirectory);
            configurator.Repository.ExtractZip(pointStorage.Path, pathToDirectory);
            foreach (BackupObject backupObject in configurator.BackupObjects)
            {
                string location = Path.Combine(pointStorage.Path, Path.GetFileName(backupObject.Path) !);
                File.Move(location, backupObject.Path);
            }

            Directory.Delete(pathToDirectory);
        }
    }
}