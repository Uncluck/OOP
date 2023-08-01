using System.IO.Compression;
using Backups.Extra.Interfaces;
using Backups.Models;
using Newtonsoft.Json;

namespace Backups.Extra.Algorithms;

public class DifferentRestoreToVersion : IRestoreToVersion
{
    [JsonProperty]
    private readonly string _path;
    public DifferentRestoreToVersion(string pathToDirectory)
    {
        _path = pathToDirectory;
    }

    public void Execute(Backup backup, int version, Configurator configurator)
    {
        var point = backup.RestorePoints.Single(point => point.Number == version);
        if (!Directory.Exists(_path))
        {
            Directory.CreateDirectory(_path);
        }

        foreach (Storage storage in point.Storages)
        {
            configurator.Repository.ExtractZip(storage.Path, _path);
        }
    }
}