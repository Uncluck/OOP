namespace Isu.Extra;

public class LessonException : Exception
{
    public LessonException()
        : base("Error")
    {
    }

    public LessonException(string message)
        : base(message)
    {
    }
}