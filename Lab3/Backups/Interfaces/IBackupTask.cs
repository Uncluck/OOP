using Backups.Models;

namespace Backups.Interfaces;

public interface IBackupTask
{
    Configurator Configurator { get; }
    Backup Backup { get; }
    BackupJob BackupJob { get; }
    void Implement();
}