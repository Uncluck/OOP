namespace Isu.Extra.Models;

public class Timetable
{
    private List<Lesson> _lessons;

    public Timetable(List<Lesson> lessons)
    {
        _lessons = lessons ?? throw new ArgumentNullException(nameof(lessons));
    }

    public IReadOnlyList<Lesson> Lessons => _lessons;
    public void AddLesson(Lesson lesson)
    {
        if (lesson is null)
            throw new IsuExtraException("The lesson is null");
        var listOfOneLesson = new List<Lesson> { lesson };
        if (_lessons.Intersect(listOfOneLesson).Any()) throw new LessonException("Timetable intersection");
        _lessons.Add(lesson);
    }
}