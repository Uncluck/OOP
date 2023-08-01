namespace Isu.Extra;

public class IsuExtraStudentException : Exception
{
    public IsuExtraStudentException()
        : base("Error")
    {
    }

    public IsuExtraStudentException(string message)
        : base(message)
    {
    }
}