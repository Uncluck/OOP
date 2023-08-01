using Backups.Algorithms;
using Backups.Interfaces;

namespace Backups.Models;

public class Configurator
{
    public Configurator(List<BackupObject> backupObjects, IRepository repository, IAlgorithm algorithm)
    {
        Algorithm = algorithm ?? throw new ArgumentNullException(nameof(algorithm));
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        BackupObjects = backupObjects ?? throw new ArgumentNullException(nameof(backupObjects));
    }

    public IAlgorithm Algorithm { get; }
    public IRepository Repository { get; }
    public List<BackupObject> BackupObjects { get; }
}