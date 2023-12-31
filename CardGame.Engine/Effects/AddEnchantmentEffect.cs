﻿using CardGame.Engine.Combats.Characters;
using CardGame.Engine.Effects.Enchantments;

namespace CardGame.Engine.Effects;

public class AddEnchantmentEffect : Effect
{
    public AddEnchantmentEffect(Enchantment enchantment)
    {
        Enchantment = enchantment;
    }

    public Enchantment Enchantment { get; }

    internal override void Resolve(CharacterCombatState source, IEnumerable<CharacterCombatState> targets, Random random)
    {
        foreach (CharacterCombatState target in targets)
        {
            target.AddEnchantment(Enchantment, source);
        }
    }
}
