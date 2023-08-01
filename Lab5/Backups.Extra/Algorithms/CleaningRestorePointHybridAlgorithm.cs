using System.IO.Compression;
using Backups.Extra.Exceptions;
using Backups.Extra.Interfaces;
using Backups.Extra.Model;
using Backups.Models;
using Newtonsoft.Json;

namespace Backups.Extra.Algorithms;

public class CleaningRestorePointHybridAlgorithm : IAlgorithmExtra
{
    [JsonProperty]
    private readonly CleaningRestorePointByAmountAlgorithm _amountAlgorithm;
    private readonly CleaningRestorePointByDateAlgorithm _dateAlgorithm;

    public CleaningRestorePointHybridAlgorithm(CleaningRestorePointByAmountAlgorithm amountAlgorithm, CleaningRestorePointByDateAlgorithm dateAlgorithm)
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

                    RemovePoint(backup, point);
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

                RemovePoint(backup, backup.RestorePoints.First());
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

                RemovePoint(backup, backup.RestorePoints.First());
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

                    RemovePoint(backup, point);
                }

                if (backup.RestorePoints.Count == 1)
                {
                    break;
                }
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