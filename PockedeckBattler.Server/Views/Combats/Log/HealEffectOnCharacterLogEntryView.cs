using CardGame.Engine.Combats.Abstractions;
using CardGame.Engine.Combats.Logs;

namespace PockedeckBattler.Server.Views.Combats.Log;

public class HealEffectOnCharacterLogEntryView : EffectOnCharacterLogEntryView
{
    public HealEffectOnCharacterLogEntryView(CharacterInCombatView character, HealReceived heal) : base(character)
    {
        Heal = heal;
    }

    public HealReceived Heal { get; }
}

public static class HealEffectOnCharacterLogEntryViewMappingExtensions
{
    public static HealEffectOnCharacterLogEntryView View(this HealEffectOnCharacterLogEntry entry)
    {
        return new HealEffectOnCharacterLogEntryView(entry.Character.View(), entry.Heal);
    }
}
