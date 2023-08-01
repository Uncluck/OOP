using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Group
{
    private readonly List<Student> _students;
    public Group(GroupName groupName)
    {
        GroupName = groupName;
        _students = new List<Student>();
    }

    public GroupName GroupName { get; }
    public CourseNumber CourseNumber { get; }

    public void AddStudent(Student student, Group group)
    {
        if (student is null) throw new ArgumentNullException(nameof(student));
        if (_students.Count == Constans.MaxGroupSize)
        {
            throw new GroupFullException("Group is full");
        }

        _students.Add(student);
    }

    public IReadOnlyList<Student> GetAllStudents()
    {
        return _students;
    }

    public Student GetNameStudent(string name)
    {
        return _students.FirstOrDefault(student => student.Name == name) ??
               throw new GroupException($"Invalid student with name");
    }

    public void RemoveStudent(Student removedStudent, Group group2)
    {
        if (!_students.Remove(removedStudent))
        {
            throw new StudentException("Student is not in the group");
        }
    }

    public bool GroupIsFull()
    {
        return _students.Count == Constans.MaxGroupSize;
    }
}