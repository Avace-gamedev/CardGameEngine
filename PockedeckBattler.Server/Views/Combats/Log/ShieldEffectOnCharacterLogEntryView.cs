using CardGame.Engine.Combats.Abstractions;
using CardGame.Engine.Combats.Logs;

namespace PockedeckBattler.Server.Views.Combats.Log;

public class ShieldEffectOnCharacterLogEntryView : EffectOnCharacterLogEntryView
{
    public ShieldEffectOnCharacterLogEntryView(CharacterInCombatView character, ShieldReceived shield) : base(character)
    {
        Shield = shield;
    }

    public ShieldReceived Shield { get; }
}

public static class ShieldEffectOnCharacterLogEntryViewMappingExtensions
{
    public static ShieldEffectOnCharacterLogEntryView View(this ShieldEffectOnCharacterLogEntry entry)
    {
        return new ShieldEffectOnCharacterLogEntryView(entry.Character.View(), entry.Shield);
    }
}
