﻿using CardGame.Engine.Combats;
using CardGame.Engine.Combats.Abstractions;
using CardGame.Engine.Combats.Characters;

namespace CardGame.Engine.Effects.Enchantments.Passive;

public class PassiveEffectInstance : IDisposable
{
    public PassiveEffectInstance(PassiveEffect effect, CharacterCombatState source, CharacterCombatState target)
    {
        Id = Guid.NewGuid();
        Effect = effect;
        Source = source;
        Target = target;
        RemainingDuration = effect.Duration;

        source.Combat.PhaseStarted += OnPhaseStarted;
    }

    public Guid Id { get; }
    public PassiveEffect Effect { get; }
    public CharacterCombatState Source { get; }
    public CharacterCombatState Target { get; }
    public int RemainingDuration { get; private set; }
    public bool HasExpired { get; private set; }

    public void Dispose()
    {
        Source.Combat.PhaseStarted -= OnPhaseStarted;
        GC.SuppressFinalize(this);
    }

    public event EventHandler? Expired;

    void OnPhaseStarted(object? _, PhaseEventArgs args)
    {
        if (args.Side == Source.Side && args.Phase == CombatSideTurnPhase.StartOfTurn)
        {
            RemainingDuration--;
            if (RemainingDuration <= 0)
            {
                HasExpired = true;
                Expired?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
