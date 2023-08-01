using Backups.Models;

namespace Backups.Extra.Interfaces;

public interface IAlgorithmExtra
{
    void Execute(Backup backup);
}