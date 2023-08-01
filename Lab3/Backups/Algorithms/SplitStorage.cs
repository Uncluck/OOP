using System.IO.Compression;
using Backups.Exceptions;
using Backups.Interfaces;
using Backups.Models;

namespace Backups.Algorithms;

public class SplitStorage : IAlgorithm
{
    public TypeOfAlgorithm Type => TypeOfAlgorithm.SplitType;
    public List<Storage> CreateBackup(Configurator configurator, int number)
    {
        if (configurator is null) throw new BackupException("Configurator is null");
        var storages = new List<Storage>();
        foreach (BackupObject backupObject in configurator.BackupObjects)
        {
            string path = Path.Combine(configurator.Repository.FullPath, $"{backupObject.Name}.zip");
            var storage = new Storage(path);
            configurator.Repository.CreateZip(path, number, backupObject);
            storages.Add(storage);
        }

        return storages;
    }
}