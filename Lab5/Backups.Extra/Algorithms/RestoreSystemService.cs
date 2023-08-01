using Backups.Extra.Entities;
using Backups.Extra.Exceptions;
using Newtonsoft.Json;

namespace Backups.Extra.Algorithms;

public class RestoreSystemService
{
    [JsonProperty]
    private string _jsonPath;

    private JsonSerializerSettings _serializerSettings = new ()
    {
        TypeNameHandling = TypeNameHandling.Auto,
        Formatting = Formatting.Indented,
    };

    public RestoreSystemService(string jsonFilePath)
    {
        if (string.IsNullOrWhiteSpace(jsonFilePath)) throw new BackupExtraException("path isn't valid");
        _jsonPath = jsonFilePath;
    }

    public void Save(BackupExtraService service)
    {
        File.WriteAllText(
            _jsonPath,
            JsonConvert.SerializeObject(
                service, _serializerSettings));
    }

    public void Load()
    {
        JsonConvert.DeserializeObject<BackupExtraService>(
            File.ReadAllText(_jsonPath),
            _serializerSettings);
    }
}