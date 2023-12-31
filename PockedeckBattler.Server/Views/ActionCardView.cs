﻿using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Cards.ActionCard;
using PockedeckBattler.Server.Views.Effects.Active;

namespace PockedeckBattler.Server.Views;

public class ActionCardView
{
    public ActionCardView(string name, string? description, int apCost, ActionCardTarget target, EffectView mainEffect, IReadOnlyList<EffectView> additionalEffects)
    {
        Name = name;
        Description = description;
        ApCost = apCost;
        Target = target;
        MainEffect = mainEffect;
        AdditionalEffects = additionalEffects;
    }

    [Required]
    public string Name { get; }

    public string? Description { get; }
    public int ApCost { get; }
    public ActionCardTarget Target { get; }

    [Required]
    public EffectView MainEffect { get; }

    [Required]
    public IReadOnlyList<EffectView> AdditionalEffects { get; }
}

public static class ActionCardViewMappingExtensions
{
    public static ActionCardView View(this ActionCard card)
    {
        return new ActionCardView(
            card.Name,
            card.Description,
            card.ApCost,
            card.Target,
            card.MainEffect.View(),
            card.AdditionalEffects.Select(e => e.View()).ToArray()
        );
    }
}
