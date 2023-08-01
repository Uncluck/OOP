using System.Security;
using System.Xml.Linq;
using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private int nextId = 100000;
    private List<Group> _groups;
    private List<Student> _students;

    public IsuService()
    {
        _groups = new List<Group>();
        _students = new List<Student>();
    }

    public Student GetStudent(int id)
    {
        return _groups
            .SelectMany(group => group.GetAllStudents())
            .FirstOrDefault(student => id == student.Id);
    }

    public Student FindStudent(string name)
    {
        return _groups
            .SelectMany(group => group.GetAllStudents())
            .FirstOrDefault(student => name == student.Name);
    }

    public IReadOnlyList<Student> FindStudents(GroupName groupName)
    {
        return _students
            .Where(student => student.GroupName == groupName)
            .ToList();
    }

    public IReadOnlyList<Student> FindStudents(CourseNumber courseNumber)
    {
        return _students
            .Where(student => student.CourseNumber == courseNumber)
            .ToList();
    }

    public Group AddGroup(GroupName name)
    {
        var newGroup = new Group(name);
        if (_groups
            .Any(group => newGroup == group))
        {
            throw new GroupException("The group has already been created");
        }

        _groups.Add(newGroup);
        return newGroup;
    }

    public Group FindGroup(GroupName groupName)
    {
        return _groups
            .FirstOrDefault(group => group.GroupName == groupName);
    }

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        return _groups
            .Where(group => group.CourseNumber == courseNumber)
            .ToList();
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        Group oldGroup = FindGroup(student.GroupName);
        if (oldGroup is null) throw new GroupException("oldGroup is null");
        if (newGroup is null) throw new GroupException("newGroup is null");
        if (newGroup.GroupIsFull()) throw new GroupException("Group is full");
        oldGroup.RemoveStudent(student, oldGroup);
        newGroup.AddStudent(student, newGroup);
        student.GroupName = newGroup.GroupName;
        student.CourseNumber = newGroup.CourseNumber;
    }

    public Student AddStudent(Group group, string name)
    {
        EnsureGroupIsValid(group.GroupName);
        if (!_groups.Contains(group))
        {
            throw new GroupException("This group isn't in  Isuservice");
        }

        var student = new Student(nextId++, name);
        group.AddStudent(student, group);
        student.GroupName = group.GroupName;
        student.CourseNumber = group.CourseNumber;
        return student;
    }

    private void EnsureGroupIsValid(GroupName groupName)
    {
        FindGroup(groupName);
        if (groupName is null)
        {
            throw new GroupException("Group is not valid");
        }
    }
}