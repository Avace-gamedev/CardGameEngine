using CardGame.Engine.Combats;
using CardGame.Engine.Effects.Enchantments;

namespace CardGame.Engine.Effects.Active;

public class AddEnchantmentEffect : ActiveEffect
{
    public AddEnchantmentEffect(Enchantment enchantment)
    {
        Enchantment = enchantment;
    }

    public Enchantment Enchantment { get; }

    public override void Resolve(CharacterCombatState source, IEnumerable<CharacterCombatState> targets)
    {
        foreach (CharacterCombatState target in targets)
        {
            target.AddEnchantment(Enchantment, source);
        }
    }
}
