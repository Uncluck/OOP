using Isu.Entities;

namespace Isu.Extra.Models;

public class IsuExtraGroup
{
    public IsuExtraGroup(Group group, Timetable timetable)
    {
        Group = group ?? throw new ArgumentNullException(nameof(group));
        Timetable = timetable ?? throw new ArgumentNullException(nameof(timetable));
    }

    public Group Group { get; }
    public Timetable Timetable { get; }
}