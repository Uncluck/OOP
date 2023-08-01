using System.IO.Compression;
using Backups.Exceptions;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Algorithms;

public class SingleStorage : IAlgorithm
{
    public TypeOfAlgorithm Type => TypeOfAlgorithm.SingleType;
    public List<Storage> CreateBackup(Configurator configurator, int number)
    {
        if (configurator is null) throw new BackupException("Configurator is null");
        string zipPath = Path.Combine(configurator.Repository.FullPath, $"RestorePoint_{number}.zip");
        foreach (BackupObject backupObject in configurator.BackupObjects)
        {
            configurator.Repository.CreateZip(zipPath, number, backupObject);
        }

        return new List<Storage>()
        {
            new (zipPath),
        };
    }
}