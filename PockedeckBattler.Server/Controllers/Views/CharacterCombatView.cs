using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Combats;
using PockedeckBattler.Server.Controllers.Views.Effects;

namespace PockedeckBattler.Server.Controllers.Views;

public class CharacterCombatView
{
    public CharacterCombatView(CharacterView character, int health, StatsModifier modifiers, PassiveEffectInstanceView[] passiveEffects)
    {
        Character = character;
        Health = health;
        Modifiers = modifiers;
        PassiveEffects = passiveEffects;
    }

    [Required]
    public CharacterView Character { get; }

    public int Health { get; }

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
            character.StatsModifier,
            character.PassiveEffects.Select(e => e.View()).ToArray()
        );
    }
}
