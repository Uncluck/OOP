namespace Isu.Exceptions;

public class GroupNameException : Exception
{
    public GroupNameException()
        : base("Error")
    {
    }

    public GroupNameException(string message)
        : base(message)
    {
    }
}