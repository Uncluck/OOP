using Backups.Models;

namespace Backups.Interfaces;

public interface IBackupJob
{
    RestorePoint Implement(Configurator configurator, int version);
}