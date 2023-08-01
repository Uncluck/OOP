using Backups.Extra.Interfaces;
using Backups.Models;

namespace Backups.Extra.Algorithms;

public class VirtualCleaningRestorePointByDateAlgorithm : IAlgorithmExtra
{
    public VirtualCleaningRestorePointByDateAlgorithm(DateTime time)
    {
        DateTime = time;
    }

    public DateTime DateTime { get; }

    public void Execute(Backup backup)
    {
        foreach (RestorePoint point in backup.RestorePoints)
        {
            if (point.Date < DateTime)
            {
                backup.RemovePoint(point);
            }

            if (backup.RestorePoints.Count == 1)
            {
                break;
            }
        }
    }
}