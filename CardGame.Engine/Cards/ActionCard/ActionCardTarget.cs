using CardGame.Engine.Combats;

namespace CardGame.Engine.Cards.ActionCard;

public enum ActionCardTarget
{
    None,

    Self,
    OtherAlly,

    FrontOpponent,
    BackOpponent,
    AllOpponents,

    FrontAlly,
    BackAlly,
    AllAllies
}

public static class CardTargetExtensions
{
    public static IEnumerable<CharacterCombatState> GetTargets(this ActionCardTarget target, CharacterCombatState source)
    {
        CombatInstance combat = source.Combat;

        IEnumerable<CharacterCombatState?> characters = target switch
        {
            ActionCardTarget.None => Enumerable.Empty<CharacterCombatState?>(),
            ActionCardTarget.Self => new[] { source },
            ActionCardTarget.OtherAlly => combat.GetAllCharacters(source.Side),
            ActionCardTarget.FrontOpponent => new[]
            {
                combat.GetCharacter(source.Side.OtherSide(), CombatPosition.Front)
            },
            ActionCardTarget.BackOpponent => new[]
            {
                combat.GetCharacter(source.Side.OtherSide(), CombatPosition.Back) ?? combat.GetCharacter(source.Side.OtherSide(), CombatPosition.Front)
            },
            ActionCardTarget.AllOpponents => combat.GetAllCharacters(source.Side.OtherSide()),
            ActionCardTarget.FrontAlly => new[] { combat.GetCharacter(source.Side, CombatPosition.Front) },
            ActionCardTarget.BackAlly => new[] { combat.GetCharacter(source.Side, CombatPosition.Back) ?? combat.GetCharacter(source.Side, CombatPosition.Front) },
            ActionCardTarget.AllAllies => combat.GetAllCharacters(source.Side),
            _ => throw new ArgumentOutOfRangeException(nameof(target), target, null)
        };

        return characters.Where(c => c != null).Select(c => c!);
    }
}
