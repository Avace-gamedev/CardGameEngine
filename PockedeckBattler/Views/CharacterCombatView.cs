using CardGame.Engine.Combats;
using CardGame.Engine.Effects.Passive;

namespace PockedeckBattler.Views;

public class CharacterCombatView
{
    public CharacterCombatView(CharacterView character, int health, StatsModifier modifiers, PassiveEffectInstance[] passiveEffects)
    {
        Character = character;
        Health = health;
        Modifiers = modifiers;
        PassiveEffects = passiveEffects;
    }

    public CharacterView Character { get; }
    public int Health { get; }
    public PassiveEffectInstance[] PassiveEffects { get; }
    public StatsModifier Modifiers { get; }
}

public static class CharacterCombatViewMappingExtensions
{
    public static CharacterCombatView View(this CharacterCombatState character)
    {
        return new CharacterCombatView(character.Character.View(), character.Health, character.StatsModifier, character.PassiveEffects.ToArray());
    }
}
