using Backups.Extra.Exceptions;
using Serilog;

namespace Backups.Extra.Model;

public class Logger
{
    private static ILogger _logger;

    public Logger(ILogger logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public static void LogInfo(string msg)
    {
        if (string.IsNullOrWhiteSpace(msg)) throw new BackupExtraException("msg isn't valid");
        _logger.Information(msg);
    }
}