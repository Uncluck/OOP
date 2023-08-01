using System.Collections.ObjectModel;

namespace Isu.Extra.Models;

public class Megafaculty
{
    private List<Ognp> _ognps;
    public Megafaculty(char specialization)
    {
        if (!char.IsUpper(specialization)) throw new IsuExtraException("Specialization isn't correct");
        Specialization = specialization;
        _ognps = new List<Ognp>();
    }

    public char Specialization { get; }
    public ReadOnlyCollection<Ognp> Ognps => _ognps.AsReadOnly();
}