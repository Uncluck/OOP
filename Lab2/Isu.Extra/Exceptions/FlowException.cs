namespace Isu.Extra;

public class FlowException : Exception
{
    public FlowException()
        : base("Error")
    {
    }

    public FlowException(string message)
        : base(message)
    {
    }
}