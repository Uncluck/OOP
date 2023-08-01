using Backups.Exceptions;
using Newtonsoft.Json;

namespace Backups.Models;

public class RestorePoint
{
    [JsonProperty]
    private const int IncorrectNumder = 0;
    private readonly List<Storage> _storages;
    public RestorePoint(List<Storage> storages, int number)
    {
        if (!storages.Any()) throw new BackupException("Don't have any saved files");
        DateTime date = DateTime.Now;
        Date = date;
        _storages = storages;
        if (number < IncorrectNumder) throw new BackupException("Number isn't correct");
        Number = number;
    }

    public DateTime Date { get; }
    public int Number { get; }
    public IReadOnlyList<Storage> Storages => _storages.AsReadOnly();
}