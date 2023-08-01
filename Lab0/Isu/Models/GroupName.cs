using System.Dynamic;
using Isu.Exceptions;

namespace Isu.Models;

public class GroupName
{
    private const int MaxCourseSize = 4;
    private const int MinCourseSize = 1;
    private const int MaxGroupLength = 5;
    private const int CourseNumberPos = 2;
    private const int MaxGroupNumber = 99;
    private readonly string _groupName;

    public GroupName(string groupName)
    {
        EnsureGroupNameIsValid(groupName);
        _groupName = groupName;
        GroupNumber = int.Parse(groupName.Substring(3, 2));
        CourseNumber = new CourseNumber(int.Parse(groupName.Substring(2, 1)));
    }

    public int GroupNumber { get; }
    private CourseNumber CourseNumber { get; }

    private void EnsureGroupNameIsValid(string groupName)
    {
        if (string.IsNullOrEmpty(groupName))
        {
            throw new GroupNameException("groupName IsNullOrEmpty");
        }

        if (groupName.Length > MaxGroupLength)
        {
            throw new GroupNameException("Length != MaxGroupLength");
        }

        if (!char.IsLetter(groupName[0]))
        {
            throw new GroupNameException("elem 0 !char");
        }

        if (!int.TryParse(groupName.AsSpan(1), out _))
        {
            throw new CourseException("1 elem isn't int");
        }

        if (GroupNumber > MaxGroupNumber)
        {
            throw new GroupException($"Invalid group number");
        }
    }
}