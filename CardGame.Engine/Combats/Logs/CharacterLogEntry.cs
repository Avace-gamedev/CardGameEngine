using CardGame.Engine.Combats.Abstractions;

namespace CardGame.Engine.Combats.Logs;

public class CharacterLogEntry
{
    public CharacterLogEntry(string name, CombatSide side)
    {
        Name = name;
        Side = side;
    }

    public string Name { get; }
    public CombatSide Side { get; }
}
