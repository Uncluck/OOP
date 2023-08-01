using Backups.Exceptions;
using Backups.Interfaces;
using Backups.Models;
using Newtonsoft.Json;

namespace Backups.Entities;

public class BackupService : IBackupService
{
    [JsonProperty]
    private readonly List<BackupTask> _backupTasks;
    private int _nextTaskId = 1;
    public BackupService()
    {
        _backupTasks = new List<BackupTask>();
    }

    public void AddBackupObject(string path, string name, int taskId)
    {
        if (string.IsNullOrEmpty(path)) throw new BackupException("path isn't valid");
        if (string.IsNullOrEmpty(name)) throw new BackupException("name isn't valid");
        var backupObject = new BackupObject(path, name);
        GetTaskById(taskId).Configurator.BackupObjects.Add(backupObject);
    }

    public void Execute(int taskId)
    {
        GetTaskById(taskId).Implement();
    }

    public int AddBackupTask(Configurator configurator, Backup backup, BackupJob backupJob, int id)
    {
        _backupTasks.Add(new BackupTask(configurator, backup, backupJob, id, _nextTaskId));
        return _nextTaskId++;
    }

    public void RemoveBackupTask(int taskId)
    {
        _backupTasks.Remove(GetTaskById(taskId));
    }

    public BackupTask GetTaskById(int taskId)
    {
        return _backupTasks.Single(task => task.TaskId == taskId);
    }
}