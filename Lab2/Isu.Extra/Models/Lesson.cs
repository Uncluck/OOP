using Isu.Extra.Models;

namespace Isu.Extra;

public class Lesson : IEquatable<Lesson>
{
    private const int IncorrectAuditorium = 0;
    public Lesson(string name, Time time, string teacher, int auditorium)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new LessonException("name isn't correct");
        Name = name;
        Time = time ?? throw new ArgumentNullException(nameof(time));
        if (string.IsNullOrEmpty(teacher))
            throw new LessonException("Teacher isn't correct");
        Teacher = teacher;
        if (auditorium <= IncorrectAuditorium)
            throw new LessonException("Auditorium mest be > 0");
        Auditorium = auditorium;
    }

    public string Name { get; }
    public string Teacher { get; }
    public int Auditorium { get; }
    public Time Time { get; }

    public bool Equals(Lesson other)
    {
        if (ReferenceEquals(null, other)) return false;
        return ReferenceEquals(this, other) || (Equals(Time.DayOfWeek, other.Time.DayOfWeek)
            && Equals(Time.NumberOfLesson, other.Time.NumberOfLesson));
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Lesson)obj);
    }

    public override int GetHashCode()
    {
        return Time.GetHashCode();
    }
}