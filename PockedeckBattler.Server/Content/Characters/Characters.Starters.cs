using CardGame.Engine.Cards.ActionCard;
using CardGame.Engine.Characters;
using CardGame.Engine.Combats;
using CardGame.Engine.Effects.Active;
using CardGame.Engine.Effects.Triggered;
using PockedeckBattler.Server.Content.Characters.Attributes;

namespace PockedeckBattler.Server.Content.Characters;

public static partial class Characters
{
    [Starter]
    public static Character Charmander { get; } = new(
        new CharacterIdentity(
            "charmander",
            "Charmander",
            "Even the newborns have flaming tails. Unfamiliar with fire, babies are said to accidentally burn themselves."
        ),
        new CharacterStatistics
        {
            MaxHealth = 39
        },
        new[]
        {
            ActionCard.Damage("Scratch", "Scratches the foe with sharp claws.", 3, ActionCardTarget.FrontOpponent, 5, Element.Neutral),
            ActionCard.Damage(
                "Ember",
                "A weak fire attack that may inflict a burn.",
                3,
                ActionCardTarget.FrontOpponent,
                5,
                Element.Fire,
                RandomEffect.Uniform(new AddTriggeredEffect(TriggeredEffect.DamageOverTime(new DamageEffect(2, Element.Fire), 3)), null, null, null)
            ),
            ActionCard.AddPassive(
                "Growl",
                "Reduces the foe's Attack.",
                3,
                ActionCardTarget.FrontOpponent,
                AddPassiveEffect.StatsModifier(new StatsModifier { DamageAdditiveModifier = -2 }, 3)
            )
        }
    );

    [Starter]
    public static Character Squirtle { get; } = new(
        new CharacterIdentity("squirtle", "Squirtle", "It takes time for the shell to form and harden after hatching. It sprays foam powerfully from its mouth."),
        new CharacterStatistics
        {
            MaxHealth = 44
        },
        new[]
        {
            ActionCard.Damage("Tackle", "A full-body charge attack.", 3, ActionCardTarget.FrontOpponent, 5, Element.Neutral),
            ActionCard.Damage("Water Gun", "Squirts water to attack the foe.", 3, ActionCardTarget.FrontOpponent, 6, Element.Water),
            ActionCard.AddPassive(
                "Tail Whip",
                "Wags the tail to lower the foe's Defense.",
                3,
                ActionCardTarget.FrontOpponent,
                AddPassiveEffect.StatsModifier(new StatsModifier { DamageReductionAdditiveModifier = -2 }, 3)
            ),
            ActionCard.Shield("Protect", "Use its shell to protect against next attacks.", 3, ActionCardTarget.Self, 10),
            ActionCard.Damage("Hydro Pump", "Blasts water at high power to strike the foe.", 7, ActionCardTarget.FrontOpponent, 36, Element.Water)
        }
    );

    [Starter]
    public static Character Bulbasaur { get; } = new(
        new CharacterIdentity("bulbasaur", "Bulbasaur", "The bulb-like pouch on its back grows larger as it ages. The pouch is filled with numerous seeds."),
        new CharacterStatistics
        {
            MaxHealth = 45
        },
        new[]
        {
            ActionCard.Damage("Tackle", "A full-body charge attack.", 3, ActionCardTarget.FrontOpponent, 5, Element.Neutral),
            ActionCard.Damage("Vine Whip", "Whips the foe with slender vines.", 3, ActionCardTarget.FrontOpponent, 6, Element.Earth),
            ActionCard.AddPassive(
                "Growth",
                "Forces the body to grow and heightens Bulbasaur's attack.",
                3,
                ActionCardTarget.FrontOpponent,
                AddPassiveEffect.StatsModifier(new StatsModifier { DamageReductionAdditiveModifier = 2 }, 3)
            ),
            ActionCard.AddTriggered(
                "Leech Seed",
                "Plants a seed on the target. It slowly drains the target's HP for the attacker.",
                3,
                ActionCardTarget.FrontOpponent,
                TriggeredEffect.DamageOverTime(new DamageEffect(2, Element.Earth) { LifeStealRatio = 1 }, 3)
            ),
            ActionCard.AddTriggered(
                "Solar Beam",
                "Gathers light energy, then blasts a bundled beam on the next turn. ",
                7,
                ActionCardTarget.AllOpponents,
                TriggeredEffect.DamageOverTime(new DamageEffect(36, Element.Earth), 0, 1, TurnTrigger.TriggerMoment.StartOfSourceTurn)
            )
        }
    );
}
