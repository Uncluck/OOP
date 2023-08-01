using Backups.Extra.Interfaces;
using Backups.Models;

namespace Backups.Extra.Algorithms;

public class VirtualCleaningRestorePointHybridAlgorithm : IAlgorithmExtra
{
    private readonly CleaningRestorePointByAmountAlgorithm _amountAlgorithm;
    private readonly CleaningRestorePointByDateAlgorithm _dateAlgorithm;

    public VirtualCleaningRestorePointHybridAlgorithm(CleaningRestorePointByAmountAlgorithm amountAlgorithm, CleaningRestorePointByDateAlgorithm dateAlgorithm)
    {
        _amountAlgorithm = amountAlgorithm;
        _dateAlgorithm = dateAlgorithm;
    }

    public void Execute(Backup backup)
    {
        if (_amountAlgorithm.Amount == 0 && !_amountAlgorithm.Amount.Equals(default))
        {
            foreach (RestorePoint point in backup.RestorePoints)
            {
                if (point.Date < _dateAlgorithm.DateTime)
                {
                    if (backup.RestorePoints.Count == 1)
                    {
                        break;
                    }

                    backup.RemovePoint(point);
                }

                if (backup.RestorePoints.Count == 1)
                {
                    break;
                }
            }
        }

        if (_amountAlgorithm.Amount != 0 && _dateAlgorithm.DateTime.Equals(default))
        {
            while (backup.RestorePoints.Count > _amountAlgorithm.Amount)
            {
                if (backup.RestorePoints.Count == 1)
                {
                   break;
                }

                backup.RemovePoint(backup.RestorePoints.First());
                if (backup.RestorePoints.Count == 1)
                {
                    break;
                }
            }
        }

        if (_amountAlgorithm.Amount == 0 || _dateAlgorithm.DateTime.Equals(default)) return;
        {
            while (backup.RestorePoints.Count > _amountAlgorithm.Amount)
            {
                if (backup.RestorePoints.Count == 1)
                {
                    break;
                }

                backup.RemovePoint(backup.RestorePoints.First());
                if (backup.RestorePoints.Count == 1)
                {
                   break;
                }
            }

            foreach (RestorePoint point in backup.RestorePoints)
            {
                if (point.Date < _dateAlgorithm.DateTime)
                {
                    if (backup.RestorePoints.Count == 1)
                    {
                        break;
                    }

                    backup.RemovePoint(point);
                }

                if (backup.RestorePoints.Count == 1)
                {
                    break;
                }
            }
        }
    }
}