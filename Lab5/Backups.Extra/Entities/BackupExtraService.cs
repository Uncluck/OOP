using Backups.Entities;
using Backups.Extra.Algorithms;
using Backups.Extra.Interfaces;
using Backups.Extra.Model;
using Backups.Interfaces;
using Backups.Models;
using Newtonsoft.Json;

namespace Backups.Extra.Entities;

public class BackupExtraService : IBackupService
{
    [JsonProperty]
    private readonly BackupService _service;
    private readonly List<BackupExtraTask> _extraTask;
    private int _nextTaskId = 1;

    public BackupExtraService()
    {
        _service = new BackupService();
        _extraTask = new List<BackupExtraTask>();
    }

    public void AddBackupObject(string path, string name, int taskId)
    {
        _service.AddBackupObject(path, name, taskId);
    }

    public void Execute(int taskId)
    {
        GetTaskById(taskId).Implement();
    }

    public int AddBackupExtraTask(Configurator configurator, Backup backup, BackupJob backupJob, int id, IAlgorithmExtra algorithmExtra, IRestoreToVersion toVersion)
    {
        _service.AddBackupTask(configurator, backup, backupJob, id);
        _extraTask.Add(new BackupExtraTask(configurator, backup, backupJob, id, _nextTaskId, algorithmExtra, toVersion, new VirtualMergePoint()));
        return _nextTaskId++;
    }

    public BackupExtraTask GetTaskById(int taskId)
    {
        return _extraTask.Single(task => task.TaskId == taskId);
    }

    public void DifferentRestore(string path, Backup backup, int id, Configurator configurator)
    {
        var restore = new DifferentRestoreToVersion(path);
        restore.Execute(backup, id, configurator);
    }

    public void OriginRestore(Backup backup, int id, Configurator configurator)
    {
        var restore = new OriginRestoreToVersion();
        restore.Execute(backup, id, configurator);
    }

    public void AmountAlgorithm(Backup backup, int amount)
    {
        var algorithm = new CleaningRestorePointByAmountAlgorithm(amount);
        algorithm.Execute(backup);
    }

    public void DateAlgorithm(Backup backup)
    {
        var algorithm = new CleaningRestorePointByDateAlgorithm(DateTime.Now);
        algorithm.Execute(backup);
    }

    public void HybridAlgorithm(Backup backup, int amount)
    {
        var algorithm = new CleaningRestorePointHybridAlgorithm(
            new CleaningRestorePointByAmountAlgorithm(amount),
            new CleaningRestorePointByDateAlgorithm(DateTime.Now));
        algorithm.Execute(backup);
    }

    public void Merge(int taskId, int version)
    {
        GetTaskById(taskId).MergePoint(version);
    }
}