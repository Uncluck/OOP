namespace Isu.Exceptions;

public class GroupFullException : Exception
{
    public GroupFullException()
        : base("Error")
    {
    }

    public GroupFullException(string message)
        : base(message)
    {
    }
}