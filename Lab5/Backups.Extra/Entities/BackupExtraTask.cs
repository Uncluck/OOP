using Backups.Entities;
using Backups.Extra.Algorithms;
using Backups.Extra.Exceptions;
using Backups.Extra.Interfaces;
using Backups.Interfaces;
using Backups.Models;
using Newtonsoft.Json;

namespace Backups.Extra.Entities;

public class BackupExtraTask : IBackupTask
{
    [JsonProperty]
    private readonly IBackupTask _oldTask;
    public BackupExtraTask(
        Configurator configurator,
        Backup backup,
        BackupJob backupJob,
        int id,
        int taskId,
        IAlgorithmExtra algorithmExtra,
        IRestoreToVersion restoreToVersion,
        IMergePoint mergePoint)
    {
        _oldTask = new BackupTask(configurator, backup, backupJob, id, taskId);
        TaskId = taskId;
        RestorePointAlg = algorithmExtra;
        RestoreToVersionAlg = restoreToVersion;
        MergePointAlg = mergePoint;
    }

    public Configurator Configurator => _oldTask.Configurator;
    public Backup Backup => _oldTask.Backup;
    public BackupJob BackupJob => _oldTask.BackupJob;
    public IAlgorithmExtra RestorePointAlg { get; }
    public IRestoreToVersion RestoreToVersionAlg { get; }
    public IMergePoint MergePointAlg { get; }
    public int TaskId { get; }
    public void Implement()
    {
        _oldTask.Implement();
    }

    public void MergePoint(int version)
    {
        MergePointAlg.Implement(Backup, version);
    }

    public void ExecuteRestoreToVersion(int version)
    {
        RestoreToVersionAlg.Execute(Backup, version, _oldTask.Configurator);
    }

    public void RestorePointAlgorithm()
    {
        RestorePointAlg.Execute(Backup);
    }
}