using Backups.Algorithms;
using Backups.Entities;
using Backups.Models;
using Xunit;

namespace Backups.Test;

public class BackupTest
{
    private readonly BackupService _service;

    public BackupTest()
    {
        _service = new BackupService();
    }

    [Fact]
    public void CreateBackupJob_CheckCountRestorePoints()
    {
        const string repositoryPath = "C:\\Users\\Дмитрий\\OOP";
        const string path = "C:\\Users\\Дмитрий\\Downloads\\";
        const string name = "Lab3_07.xlsx";
        const string name2 = "Lab3_11.docx";
        var backupService = new BackupService();
        var repository = new InMemoryRepository(repositoryPath);
        var configurator = new Configurator(new List<BackupObject>(), repository, new SingleStorage());
        int taskNumber = backupService.AddBackupTask(configurator, new Backup(new List<RestorePoint>()), new BackupJob(), 1);
        backupService.AddBackupObject(path, name, taskNumber);
        backupService.AddBackupObject(path, name2, taskNumber);
        backupService.Execute(taskNumber);
        backupService.Execute(taskNumber);
        Assert.Equal(2, backupService.GetTaskById(taskNumber).Backup.RestorePoints.Count);
    }
}