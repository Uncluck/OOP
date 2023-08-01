using Backups.Algorithms;
using Backups.Entities;
using Backups.Extra.Algorithms;
using Backups.Extra.Entities;
using Backups.Models;
using Newtonsoft.Json;
using Xunit;

namespace Backups.Extra.Test;

public class BackupExtraTest
{
    [JsonProperty]
    private readonly BackupExtraService _service;

    public BackupExtraTest()
    {
        _service = new BackupExtraService();
    }

    [Fact]
    public void CreateBackupJob_CheckCountRestorePoints()
    {
        var restoreSystem = new RestoreSystemService("C:\\Users\\Дмитрий\\OneDrive\\Документы\\GitHub\\Uncluck\\Lab5\\Backups.Extra\\RestoreFile.json");
        const string repositoryPath = "C:\\Users\\Дмитрий\\OOP";
        const string path = "C:\\Users\\Дмитрий\\Downloads\\";
        const string name = "Lab3_07.xlsx";
        var repository = new InMemoryRepository(repositoryPath);
        var backup = new Backup(new List<RestorePoint>());
        var configurator = new Configurator(new List<BackupObject>(), repository, new SingleStorage());
        int taskNumber = _service.AddBackupExtraTask(configurator, backup, new BackupJob(), 1, new CleaningRestorePointByAmountAlgorithm(3), new OriginRestoreToVersion());
        _service.AddBackupObject(path, name, taskNumber);
        _service.Execute(taskNumber);
        _service.Execute(taskNumber);
        _service.Execute(taskNumber);
        _service.Merge(1, 1);
        Assert.Equal(1, backup.RestorePoints.Count);
    }
}