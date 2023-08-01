using Backups.Algorithms;
using Backups.Entities;
using Backups.Extra.Algorithms;
using Backups.Extra.Entities;
using Backups.Models;
using Newtonsoft.Json;

namespace Backups.Extra
{
    internal class Program
    {
        private static void Main()
        {
            var restoreSystem = new RestoreSystemService("C:\\Users\\Дмитрий\\OneDrive\\Документы\\GitHub\\Uncluck\\Lab5\\Backups.Extra\\RestoreFile.json");
            const string repositoryPath = "C:\\Users\\Дмитрий\\OOP";
            const string path = "C:\\Users\\Дмитрий\\Downloads\\";
            const string name = "Lab3_07.xlsx";
            var service = new BackupExtraService();
            var repository = new Repository(repositoryPath);
            var backup = new Backup(new List<RestorePoint>());
            var configurator = new Configurator(new List<BackupObject>(), repository, new SingleStorage());
            int taskNumber = service.AddBackupExtraTask(configurator, backup, new BackupJob(), 1, new CleaningRestorePointByAmountAlgorithm(3), new OriginRestoreToVersion());
            service.AddBackupObject(path, name, taskNumber);
            service.Execute(taskNumber);
            service.DifferentRestore(repositoryPath, backup, 1, configurator);
            restoreSystem.Load();
            restoreSystem.Save(service);
        }
    }
}