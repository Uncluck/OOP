namespace Isu.Extra;

public class OgnpException : Exception
{
    public OgnpException()
        : base("Error")
    {
    }

    public OgnpException(string message)
        : base(message)
    {
    }
}