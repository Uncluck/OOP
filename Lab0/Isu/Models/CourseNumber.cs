using Isu.Exceptions;

namespace Isu.Models;

public class CourseNumber
{
    private const int MinCourseNumber = 1;
    private const int MaxCourseNumber = 4;

    public CourseNumber(int courseNumber)
    {
        if (courseNumber is < MinCourseNumber or > MaxCourseNumber)
        {
            throw new CourseException($"Course number is invalid {courseNumber}");
        }

        Number = courseNumber;
    }

    public int Number { get; }
}