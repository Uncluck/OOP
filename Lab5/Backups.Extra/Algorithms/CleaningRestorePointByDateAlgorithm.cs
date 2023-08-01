using System.IO.Compression;
using Backups.Extra.Exceptions;
using Backups.Extra.Interfaces;
using Backups.Extra.Model;
using Backups.Models;

namespace Backups.Extra.Algorithms;

public class CleaningRestorePointByDateAlgorithm : IAlgorithmExtra
{
    public CleaningRestorePointByDateAlgorithm(DateTime time)
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
                RemovePoint(backup, point);
            }

            if (backup.RestorePoints.Count == 1)
            {
                break;
            }
        }
    }

    private void RemovePoint(Backup backup, RestorePoint point)
    {
        backup.RemovePointFromSystem(point);

        backup.RemovePoint(point);
        Logger.LogInfo($"Point - {point.Number} was deleted");
    }
}