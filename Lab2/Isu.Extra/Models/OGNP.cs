using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra;

public class Ognp
{
    private List<Flow> _flows;
    public Ognp(string name, char specialization)
    {
        if (string.IsNullOrEmpty(name)) throw new OgnpException("name isn't correct");
        Name = name;
        if (!char.IsUpper(specialization)) throw new IsuExtraException("Specialization isn't correct");
        Specialization = specialization;
        _flows = new List<Flow>();
    }

    public string Name { get; }
    public char Specialization { get; }

    public IReadOnlyList<Flow> Flows => _flows.AsReadOnly();

    public void AddFlow(Flow flow)
    {
        if (flow is null)
        {
            throw new OgnpException("The flow is null");
        }

        _flows.Add(flow);
    }

    public void AddStudent(IsuExtraStudent student, Timetable timetable, Flow flow)
    {
        if (CheckSpecilization(student, Specialization))
            throw new OgnpException("Student's faculty the same as OGNP");
        if (flow.IsSuitableFlow(timetable))
        {
                flow.AddStudent(student);
                student.AddStudentToOgnp(flow);
        }
        else
        {
            throw new IsuExtraException("The timetable matches");
        }
    }

    private bool CheckSpecilization(IsuExtraStudent student, char ognpSpecialization)
    {
        char specializationOfGroupName = char.Parse(student.GroupName.ToString().Substring(0, 1));
        return specializationOfGroupName == ognpSpecialization;
    }
}