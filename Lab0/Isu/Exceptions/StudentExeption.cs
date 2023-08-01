namespace Isu.Exceptions;

public class StudentException : Exception
{
    public StudentException()
        : base("Error")
    {
    }

    public StudentException(string message)
        : base(message)
    {
    }
}