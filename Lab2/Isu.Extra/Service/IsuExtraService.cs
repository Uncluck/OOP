using Isu.Entities;
using Isu.Extra.Models;
using Isu.Models;
using Isu.Services;

namespace Isu.Extra.Service;

public class IsuExtraService : IIsuExtraService
{
    private IsuService _isuService;
    private List<IsuExtraStudent> _students;
    private List<Ognp> _ognps;
    private List<IsuExtraGroup> _groups;

    public IsuExtraService(IsuService isuService)
    {
        _students = new List<IsuExtraStudent>();
        _ognps = new List<Ognp>();
        _isuService = isuService;
        _groups = new List<IsuExtraGroup>();
    }

    public Ognp AddOgnp(string nameOgnp, char specialization)
    {
        var ognp = new Ognp(nameOgnp, specialization);
        _ognps.Add(ognp);
        return ognp;
    }

    public IsuExtraStudent AddStudent(Group groupStudent, string nameStudent)
    {
        var isuStudent = _isuService.AddStudent(groupStudent, nameStudent);
        var student = new IsuExtraStudent(isuStudent, new List<Flow>(0));
        _students.Add(student);
        return student;
    }

    public Flow AddFlow(string nameFlow, Timetable timetable, string nameOgnp)
    {
        var flowOgnp = new Flow(nameFlow, timetable);
        GetOgnp(nameOgnp).AddFlow(flowOgnp);
        return flowOgnp;
    }

    public IsuExtraGroup AddGroup(GroupName groupName, Timetable timetable)
    {
        var oldGroup = _isuService.AddGroup(groupName);
        var newGroup = new IsuExtraGroup(oldGroup, timetable);
        _groups.Add(newGroup);
        return newGroup;
    }

    public Ognp FindOgnp(string nameOgnp)
    {
        return _ognps.SingleOrDefault(ognp1 => ognp1.Name == nameOgnp);
    }

    public Ognp GetOgnp(string nameOgnp)
    {
        return FindOgnp(nameOgnp) ?? throw new IsuExtraException("ognp is not valid");
    }

    public void AddStudentToOgnp(IsuExtraStudent student, string ognpName, Timetable timetable, Flow flow)
    {
        if (student.FlowCount < 0 || student.FlowCount >= 2) throw new IsuExtraException("Student already have 2 OGNP");
        GetOgnp(ognpName).AddStudent(student, timetable, flow);
    }

    public void RemoveStudentFromOgnp(IsuExtraStudent student, Flow flow)
    {
        switch (student.FlowCount)
        {
            case 0:
                throw new IsuExtraException("Student have 0 OGNP");
            case > 0 and <= 2:
                flow.RemoveStudent(student);
                student.RemoveStudentFromOgnp(flow);
                break;
        }
    }

    public IReadOnlyList<IsuExtraStudent> GetStudentsFromOgnp(Ognp ognp)
    {
        if (ognp is null) throw new IsuExtraException("ognp is null");
        return ognp.Flows.SelectMany(ognpFlow => ognpFlow.AllStudents).ToList();
    }

    public IReadOnlyList<Flow> GetFlowsFromOgnp(Ognp ognp)
    {
        if (ognp is null) throw new IsuExtraException("ognp is null");
        return ognp.Flows;
    }

    public IReadOnlyList<IsuExtraStudent> GetStudentsWithoutOgnp()
    {
        return _students.Where(student => student.FlowCount == 0).ToList();
    }
}