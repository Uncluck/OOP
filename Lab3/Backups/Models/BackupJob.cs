using Backups.Exceptions;
using Backups.Interfaces;

namespace Backups.Models;

public class BackupJob : IBackupJob
{
    public RestorePoint Implement(Configurator configurator, int version)
    {
        List<Storage> storages = configurator.Algorithm.CreateBackup(configurator, version);
        return new RestorePoint(storages, version);
    }
}