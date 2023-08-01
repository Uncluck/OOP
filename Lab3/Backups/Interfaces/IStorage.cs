using Backups.Models;

namespace Backups.Interfaces;

public interface IStorage
{
    string Path { get; }
}