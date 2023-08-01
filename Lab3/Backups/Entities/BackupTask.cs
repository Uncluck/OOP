using Backups.Exceptions;
using Backups.Interfaces;
using Backups.Models;
using Newtonsoft.Json;

namespace Backups.Entities;

public class BackupTask : IBackupTask
{
    [JsonProperty]
    private int _number;
    public BackupTask(Configurator configurator, Backup backup, BackupJob backupJob, int id, int taskId)
    {
        Configurator = configurator ?? throw new BackupException("Configurator isn't valid");
        Backup = backup ?? throw new BackupException("Backup isn't valid");
        BackupJob = backupJob ?? throw new BackupException("BackupJob isn't valid");
        _number = id;
        TaskId = taskId;
    }

    public int TaskId { get; }
    public Configurator Configurator { get; }
    public Backup Backup { get; }
    public BackupJob BackupJob { get; }

    public void Implement()
    {
        RestorePoint restorePoint = BackupJob.Implement(Configurator, _number);
        Backup.AddPoint(restorePoint);
        _number++;
    }
}