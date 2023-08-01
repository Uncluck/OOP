using Isu.Entities;
using Isu.Extra.Models;
using Isu.Extra.Service;
using Isu.Models;
using Isu.Services;
using Xunit;
namespace Isu.Extra.Test;

public class IsuExtraTests
{
    private readonly IsuExtraService isu;

    public IsuExtraTests()
    {
        isu = new IsuExtraService(new IsuService());
    }

    [Fact]
    public void AddTimetable_HasTimetable()
    {
        var lessons = new List<Lesson>();
        var timetable = new Timetable(lessons);
        timetable.AddLesson(new Lesson("Physics", new Time(2, 2), "Raphael", 765));
        var groupName = new GroupName("M3211");
        IsuExtraGroup group = isu.AddGroup(groupName, timetable);
        Assert.Equal("Physics", group.Timetable.Lessons[0].Name);
        Assert.Equal("Raphael", group.Timetable.Lessons[0].Teacher);
        Assert.Equal(2, group.Timetable.Lessons[0].Time.DayOfWeek);
    }

    [Fact]
    public void AddStudentsToOGNP_HasStudentOnOGNP()
    {
        var ognp = isu.AddOgnp("PhysicsAndMathematics", 'L');
        List<Lesson> lessons = new List<Lesson>();
        List<Lesson> lessons2 = new List<Lesson>();
        List<Lesson> lessons3 = new List<Lesson>();
        var timetable = new Timetable(lessons);
        var timetable2 = new Timetable(lessons2);
        var timetable3 = new Timetable(lessons3);
        timetable.AddLesson(new Lesson("Physics", new Time(2, 2), "Tank", 1234));
        timetable2.AddLesson(new Lesson("Physics", new Time(1, 2), "Potato", 234));
        timetable3.AddLesson(new Lesson("Physics", new Time(2, 2), "Pencil", 311));
        var flowOGNP = isu.AddFlow("First", timetable, "PhysicsAndMathematics");
        var flowOGNP2 = isu.AddFlow("Second", timetable, "PhysicsAndMathematics");
        var groupName = new GroupName("M3211");
        IsuExtraGroup group = isu.AddGroup(groupName, timetable2);
        var groupName2 = new GroupName("K3211");
        IsuExtraGroup group2 = isu.AddGroup(groupName2, timetable3);
        var student = isu.AddStudent(group.Group, "KHK");
        var student2 = isu.AddStudent(group2.Group, "KKK");
        isu.AddStudentToOgnp(student, "PhysicsAndMathematics", timetable2, flowOGNP);
        Assert.Throws<IsuExtraException>(() =>
        {
            isu.AddStudentToOgnp(student2, "PhysicsAndMathematics", timetable, flowOGNP2);
        });
    }

    [Fact]
    public void RemoveStudent_HasStudent()
    {
        var ognp = isu.AddOgnp("PivoVarenie", 'L');
        List<Lesson> lessons = new List<Lesson>();
        List<Lesson> lessons2 = new List<Lesson>();
        List<Lesson> lessons3 = new List<Lesson>();
        var timetable = new Timetable(lessons);
        var timetable2 = new Timetable(lessons2);
        var timetable3 = new Timetable(lessons3);
        timetable.AddLesson(new Lesson("Mathematics", new Time(2, 2), "Erevan", 1245));
        timetable2.AddLesson(new Lesson("Mathematics", new Time(1, 2), "Ibragim", 567));
        timetable3.AddLesson(new Lesson("Mathematics", new Time(2, 2), "Gopnik", 311));
        var flowOGNP = isu.AddFlow("First", timetable, "PivoVarenie");
        var groupName = new GroupName("M3211");
        IsuExtraGroup group = isu.AddGroup(groupName, timetable);
        var groupName2 = new GroupName("K3211");
        IsuExtraGroup group2 = isu.AddGroup(groupName2, timetable3);
        var student = isu.AddStudent(group.Group, "Alexander Makedonskii");
        var student2 = isu.AddStudent(group2.Group, "Chiginkhan");
        isu.AddStudentToOgnp(student, "PivoVarenie", timetable2, flowOGNP);
        isu.RemoveStudentFromOgnp(student, flowOGNP);
        Assert.Throws<IsuExtraException>(() =>
        {
            isu.RemoveStudentFromOgnp(student2, flowOGNP);
        });
    }

    [Fact]
    public void GetFlowAndStudentFromOGNPAndGetStudentwithoutOGNP_HasFlowAndStudentFromOGNPAndHasStudentwithoutOGNP()
    {
        var ognp = isu.AddOgnp("Badminton", 'K');
        List<Lesson> lessons = new List<Lesson>();
        List<Lesson> lessons2 = new List<Lesson>();
        List<Lesson> lessons3 = new List<Lesson>();
        var timetable = new Timetable(lessons);
        var timetable2 = new Timetable(lessons2);
        var timetable3 = new Timetable(lessons3);
        timetable.AddLesson(new Lesson("OOP", new Time(1, 2), "LOX", 456));
        timetable2.AddLesson(new Lesson("OOP", new Time(2, 3), "Abdula", 2039));
        timetable3.AddLesson(new Lesson("OOP", new Time(2, 3), "Bob", 8594));
        var flowOGNP = isu.AddFlow("First", timetable2, "Badminton");
        var flowOGNP2 = isu.AddFlow("Second", timetable2, "Badminton");
        var groupName = new GroupName("M3211");
        IsuExtraGroup group = isu.AddGroup(groupName, timetable);
        var groupName2 = new GroupName("K3211");
        IsuExtraGroup group2 = isu.AddGroup(groupName2, timetable3);
        var student = isu.AddStudent(group.Group, "Ronin");
        var student2 = isu.AddStudent(group2.Group, "Samurai");
        var student3 = isu.AddStudent(group2.Group, "Shogun");
        var studetsWithoutOGNP = new List<IsuExtraStudent>();
        studetsWithoutOGNP.Add(student3);
        var students = new List<IsuExtraStudent>();
        students.Add(student);
        students.Add(student2);
        isu.AddStudentToOgnp(student, "Badminton", timetable, flowOGNP);
        isu.AddStudentToOgnp(student2, "Badminton", timetable, flowOGNP2);
        Assert.Equal(ognp.Flows, isu.GetFlowsFromOgnp(ognp));
        Assert.Equal(students, isu.GetStudentsFromOgnp(ognp));
        Assert.Equal(studetsWithoutOGNP, isu.GetStudentsWithoutOgnp());
    }
}