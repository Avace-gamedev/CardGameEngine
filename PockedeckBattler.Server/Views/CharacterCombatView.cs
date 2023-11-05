using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Combats;
using PockedeckBattler.Server.Views.Effects.Enchantments;

namespace PockedeckBattler.Server.Views;

public class CharacterCombatView
{
    public CharacterCombatView(CharacterView character, int health, int shield, EnchantmentInstanceView[] enchantments)
    {
        Character = character;
        Health = health;
        Shield = shield;
        Enchantments = enchantments;
    }

    [Required]
    public CharacterView Character { get; }

    public int Health { get; }

    public int Shield { get; }

    [Required]
    public EnchantmentInstanceView[] Enchantments { get; }
}

public static class CharacterCombatViewMappingExtensions
{
    public static CharacterCombatView View(this CharacterCombatState character)
    {
        return new CharacterCombatView(character.Character.View(), character.Health, character.Shield, character.Enchantments.Select(e => e.View()).ToArray());
    }
}
