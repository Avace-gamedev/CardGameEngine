using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Combats;
using PockedeckBattler.Server.Views.Effects;

namespace PockedeckBattler.Server.Views;

public class CharacterCombatView
{
    public CharacterCombatView(CharacterView character, int health, int shield, StatsModifier modifiers, PassiveEffectInstanceView[] passiveEffects)
    {
        Character = character;
        Health = health;
        Shield = shield;
        Modifiers = modifiers;
        PassiveEffects = passiveEffects;
    }

    [Required]
    public CharacterView Character { get; }

    public int Health { get; }

    public int Shield { get; }

    [Required]
    public PassiveEffectInstanceView[] PassiveEffects { get; }

    [Required]
    public StatsModifier Modifiers { get; }
}

public static class CharacterCombatViewMappingExtensions
{
    public static CharacterCombatView View(this CharacterCombatState character)
    {
        return new CharacterCombatView(
            character.Character.View(),
            character.Health,
            character.Shield,
            character.StatsModifier,
            character.PassiveEffects.Select(e => e.View()).ToArray()
        );
    }
}
