using Backups.Extra.Exceptions;
using Backups.Extra.Interfaces;
using Backups.Models;

namespace Backups.Extra.Algorithms;

public class VirtualCleaningRestorePointByAmountAlgorithm : IAlgorithmExtra
{
    private const int IncorrectNumber = 0;

    public VirtualCleaningRestorePointByAmountAlgorithm(int amount)
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
            backup.RemovePoint(backup.RestorePoints.First());
        }
    }
}