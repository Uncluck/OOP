namespace Isu.Extra.Models;

public class Time
{
    private const int FirstDayOfWeek = 1;
    private const int LastDayOfWeek = 7;
    private const int FirstNumberOfLesson = 1;
    private const int LastNumberOfLesson = 7;
    public Time(int dayOfWeek, int numberOfLesson)
    {
        if (numberOfLesson is < FirstNumberOfLesson or > LastNumberOfLesson)
            throw new IsuExtraException("The number of lesson isn't correct");
        if (dayOfWeek is > FirstDayOfWeek and < LastDayOfWeek)
        {
            DayOfWeek = dayOfWeek;
        }

        NumberOfLesson = numberOfLesson;
    }

    public int DayOfWeek { get; }
    public int NumberOfLesson { get; }
}