namespace Isu.Exceptions;

public class GroupException : Exception
{
    public GroupException()
        : base("Error")
    {
    }

    public GroupException(string message)
        : base(message)
    {
    }
}