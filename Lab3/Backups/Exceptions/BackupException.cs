namespace Backups.Exceptions;

public class BackupException : Exception
{
    public BackupException()
    {
    }

    public BackupException(string massage)
        : base(massage)
    {
    }
}