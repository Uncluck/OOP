namespace Isu.Exceptions;

public class CourseException : Exception
{
    public CourseException()
        : base("Error")
    {
    }

    public CourseException(string message)
        : base(message)
    {
    }
}