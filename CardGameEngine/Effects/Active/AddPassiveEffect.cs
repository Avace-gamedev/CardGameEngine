using CardGameEngine.Combats;
using CardGameEngine.Effects.Passive;
using CardGameEngine.Effects.Triggered;

namespace CardGameEngine.Effects.Active;

public class AddPassiveEffect : ActiveEffect
{
    public AddPassiveEffect(PassiveEffect effect)
    {
        Effect = effect;
    }

    public PassiveEffect Effect { get; }

    public override void Resolve(CharacterCombatState source, IEnumerable<CharacterCombatState> targets)
    {
        foreach (CharacterCombatState target in targets)
        {
            target.AddPassiveEffect(new PassiveEffectInstance(Effect, source));
        }
    }

    public static AddPassiveEffect DamageOverTime(int damage, Element element, int duration, TurnMoment triggerMoment = TurnMoment.StartOfTurn)
    {
        return new AddPassiveEffect(new TriggeredEffect(new DamageEffect(damage, element), new TurnTrigger(triggerMoment), duration));
    }

    public static AddPassiveEffect StatsModifier(StatsModifier modifier, int duration)
    {
        return new AddPassiveEffect(new PassiveStatsModifier(modifier, duration));
    }
}
