namespace Backups.Extra.Exceptions;

public class BackupExtraException : Exception
{
    public BackupExtraException()
    {
    }

    public BackupExtraException(string massage)
        : base(massage)
    {
    }
}