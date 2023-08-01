namespace Isu.Extra;

public class IsuExtraException : Exception
{
    public IsuExtraException()
        : base("Error")
    {
    }

    public IsuExtraException(string message)
        : base(message)
    {
    }
}