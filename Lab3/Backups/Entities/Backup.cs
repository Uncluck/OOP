using System.IO.Compression;
using Backups.Exceptions;
using Newtonsoft.Json;

namespace Backups.Models;

public class Backup
{
    [JsonProperty]
    private readonly List<RestorePoint> _restorePoints;
    public Backup(List<RestorePoint> restorePoints)
    {
        _restorePoints = restorePoints ?? throw new ArgumentNullException(nameof(restorePoints));
    }

    public IReadOnlyList<RestorePoint> RestorePoints => _restorePoints;
    public void AddPoint(RestorePoint restorePoint)
    {
        if (restorePoint is null) throw new BackupException("restorePoint isn't valid");
        _restorePoints.Add(restorePoint);
    }

    public void RemovePoint(RestorePoint restorePoint)
    {
        if (restorePoint is null) throw new BackupException("restorePoint isn't valid");
        _restorePoints.Remove(restorePoint);
    }

    public void RemovePointFromSystem(RestorePoint point)
    {
        using ZipArchive zipArchive = ZipFile.Open(RestorePoints.First().ToString() !, ZipArchiveMode.Update);
        string nameDeleteFile = point.Storages.First().Path;
        File.Delete(nameDeleteFile);
    }
}