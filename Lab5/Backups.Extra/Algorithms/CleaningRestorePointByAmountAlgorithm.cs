using System.IO.Compression;
using Backups.Extra.Exceptions;
using Backups.Extra.Interfaces;
using Backups.Extra.Model;
using Backups.Models;
using Newtonsoft.Json;

namespace Backups.Extra.Algorithms;

public class CleaningRestorePointByAmountAlgorithm : IAlgorithmExtra
{
    [JsonProperty]
    private const int IncorrectNumber = 0;

    public CleaningRestorePointByAmountAlgorithm(int amount)
    {
        if (amount < IncorrectNumber) throw new BackupExtraException("amount isn't valid");
        Amount = amount;
    }

    public int Amount { get; }

    public void Execute(Backup backup)
    {
        while (backup.RestorePoints.Count > Amount)
        {
            if (backup.RestorePoints.Count == 1) throw new BackupExtraException("Can't delete all Restore points");
            RemovePoint(backup, backup.RestorePoints.First());
        }
    }

    private void RemovePoint(Backup backup, RestorePoint point)
    {
        backup.RemovePointFromSystem(point);

        backup.RemovePoint(point);
        Logger.LogInfo($"Point - {point.Number} was deleted");
    }
}