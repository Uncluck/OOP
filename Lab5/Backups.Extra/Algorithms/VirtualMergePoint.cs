using Backups.Extra.Interfaces;
using Backups.Models;

namespace Backups.Extra.Algorithms;

public class VirtualMergePoint : IMergePoint
{
    public void Implement(Backup backup, int version)
    {
        var points = backup.RestorePoints.Skip(version).ToList();
        foreach (RestorePoint point in points)
        {
            backup.RemovePoint(point);
        }
    }
}