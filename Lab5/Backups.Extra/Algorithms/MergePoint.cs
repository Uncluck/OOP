using System.IO.Compression;
using Backups.Algorithms;
using Backups.Entities;
using Backups.Extra.Interfaces;
using Backups.Extra.Model;
using Backups.Models;

namespace Backups.Extra.Algorithms;

public class MergePoint : IMergePoint
{
    public void Implement(Backup backup, int version)
    {
        var points = backup.RestorePoints.Skip(version).ToList();
        foreach (RestorePoint point in points)
        {
            RemovePoint(backup, point);
        }
    }

    private void RemovePoint(Backup backup, RestorePoint point)
    {
        backup.RemovePointFromSystem(point);

        backup.RemovePoint(point);
        Logger.LogInfo($"Point - {point.Number} was deleted");
    }
}