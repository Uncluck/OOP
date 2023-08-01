using Isu.Entities;
using Isu.Models;

namespace Isu.Extra.Models;

public class IsuExtraStudent
{
    private Student _student;
    private List<Flow> _flows;

    public IsuExtraStudent(Student student, List<Flow> flows)
    {
        _flows = flows ?? throw new ArgumentNullException(nameof(flows));
        _student = student ?? throw new ArgumentNullException(nameof(student));
    }

    public int FlowCount => _flows.Count;
    public int Id => _student.Id;
    public GroupName GroupName => _student.GroupName;

    internal void AddStudentToOgnp(Flow flow)
    {
        if (flow is null) throw new IsuExtraStudentException("flow isn't valid");
        _flows.Add(flow);
    }

    internal void RemoveStudentFromOgnp(Flow flow)
    {
        if (!_flows.Contains(flow))
        {
            throw new IsuExtraStudentException("The student isn't in this flow");
        }

        _flows.Remove(flow);
    }
}