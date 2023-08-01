using Backups.Models;

namespace Backups.Extra.Interfaces;

public interface IRestoreToVersion
{
    void Execute(Backup backup, int version, Configurator configurator);
}