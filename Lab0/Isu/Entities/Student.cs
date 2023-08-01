using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Student
{
    private const int IncorrectId = 0;
    public Student(int id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new StudentException("Name is not valid");
        }

        if (id <= IncorrectId)
        {
            throw new StudentException("Id must be > 0");
        }

        Name = name;
        Id = id;
    }

    public string Name { get; }

    public int Id { get; }
    public CourseNumber CourseNumber { get; internal set; }
    public GroupName GroupName { get; internal set; }
}