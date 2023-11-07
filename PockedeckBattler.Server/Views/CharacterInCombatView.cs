using CardGame.Engine.Combats.Abstractions;
using CardGame.Engine.Combats.Characters;
using CardGame.Engine.Combats.Logs;

namespace PockedeckBattler.Server.Views;

public class CharacterInCombatView
{
    public CharacterInCombatView(string name, CombatSide side)
    {
        Name = name;
        Side = side;
    }

    public string Name { get; }
    public CombatSide Side { get; }
}

public static class CharacterInCombatMappingExtensions
{
    public static CharacterInCombatView InCombatView(this CharacterCombatState character)
    {
        return new CharacterInCombatView(character.Character.Identity.Name, character.Side);
    }

    public static CharacterInCombatView View(this CharacterLogEntry character)
    {
        return new CharacterInCombatView(character.Name, character.Side);
    }
}
