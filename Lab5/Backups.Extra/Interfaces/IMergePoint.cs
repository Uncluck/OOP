using Backups.Models;

namespace Backups.Extra.Interfaces;

public interface IMergePoint
{
    void Implement(Backup backup, int version);
}