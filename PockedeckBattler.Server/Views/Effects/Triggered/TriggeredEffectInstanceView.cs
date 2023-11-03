﻿using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Effects.Triggered;

namespace PockedeckBattler.Server.Views.Effects.Triggered;

public class TriggeredEffectInstanceView
{
    public TriggeredEffectInstanceView(Guid id, TriggeredEffectView effect, string source, TriggerStateView triggerState)
    {
        Id = id;
        Effect = effect;
        Source = source;
        TriggerState = triggerState;
    }

    public Guid Id { get; }

    [Required]
    public TriggeredEffectView Effect { get; }

    public string Source { get; }
    public TriggerStateView TriggerState { get; }
}

public static class TriggeredEffectInstanceMappingExtensions
{
    public static TriggeredEffectInstanceView View(this TriggeredEffectInstance instance)
    {
        return new TriggeredEffectInstanceView(instance.Id, instance.Effect.View(), instance.Source.Character.Identity.Name, instance.TriggerState.View());
    }
}
