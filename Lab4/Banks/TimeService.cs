namespace Banks;

public class TimeService
{
    public TimeService()
    {
        CurrentDateTime = DateTime.Now;
    }

    public DateTime CurrentDateTime { get; internal set; }

    public DateTime SkipDay(int days)
    {
        CurrentDateTime = CurrentDateTime.AddDays(days);
        return CurrentDateTime;
    }
}