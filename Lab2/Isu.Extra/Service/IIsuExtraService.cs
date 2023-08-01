using Isu.Entities;
using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Service;

public interface IIsuExtraService
{
    Ognp AddOgnp(string nameOgnp, char specialization);
    IsuExtraStudent AddStudent(Group groupStudent, string nameStudent);
    Flow AddFlow(string nameFlow, Timetable timetable, string nameOgnp);
    IsuExtraGroup AddGroup(GroupName groupName, Timetable timetable);
    Ognp FindOgnp(string nameOgnp);
    Ognp GetOgnp(string nameOgnp);
    void AddStudentToOgnp(IsuExtraStudent student, string ognpName, Timetable timetable, Flow flow);
    void RemoveStudentFromOgnp(IsuExtraStudent student, Flow flow);
    IReadOnlyList<IsuExtraStudent> GetStudentsFromOgnp(Ognp ognp);
    IReadOnlyList<Flow> GetFlowsFromOgnp(Ognp ognp);
    IReadOnlyList<IsuExtraStudent> GetStudentsWithoutOgnp();
}