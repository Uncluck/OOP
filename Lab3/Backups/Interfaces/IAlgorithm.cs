using Backups.Models;

namespace Backups.Interfaces
{
    public interface IAlgorithm
    {
        List<Storage> CreateBackup(Configurator configurator, int number);
    }
}