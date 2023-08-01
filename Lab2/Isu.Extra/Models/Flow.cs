using Isu.Entities;
using Isu.Extra.Models;

namespace Isu.Extra;

public class Flow
{
    private const int MaxNumberOfStudents = 30;
    private List<IsuExtraStudent> _allStudents;
    public Flow(string flowName, Timetable timetable)
    {
        if (string.IsNullOrWhiteSpace(flowName)) throw new FlowException("name isn't correct");
        Name = flowName;
        _allStudents = new List<IsuExtraStudent>();
        Timetable = timetable ?? throw new ArgumentNullException(nameof(timetable));
    }

    public string Name { get; }
    public Timetable Timetable { get; }
    internal List<IsuExtraStudent> AllStudents => _allStudents;

    public void AddStudent(IsuExtraStudent student)
    {
        if (student is null) throw new FlowException("student is not valid");
        if (_allStudents.FirstOrDefault(fStudent => fStudent.Id == student.Id) is not null)
            throw new FlowException("Student has been ia flow");
        if (_allStudents.Count >= MaxNumberOfStudents)
            throw new FlowException("Flow has max number of students");
        if (student.FlowCount == 2)
        {
            throw new FlowException("Student chose > 2 OGNP");
        }

        _allStudents.Add(student);
    }

    public bool IsSuitableFlow(Timetable timetable)
    {
        return !Timetable.Lessons.Intersect(timetable.Lessons).Any();
    }

    public void RemoveStudent(IsuExtraStudent removeStudent)
    {
        if (!_allStudents.Remove(removeStudent))
        {
            throw new FlowException("No such student in flow");
        }
    }
}