using CardGame.Engine.Combats;
using CardGame.Engine.Combats.Abstractions;
using CardGame.Engine.Combats.Characters;

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
    public static IEnumerable<CharacterCombatState> GetTargets(this CombatState combat, CharacterCombatState source, ActionCardTarget target)
    {
        IEnumerable<CharacterCombatState?> characters = target switch
        {
            ActionCardTarget.None => Enumerable.Empty<CharacterCombatState?>(),
            ActionCardTarget.Self => new[] { source },
            ActionCardTarget.OtherAlly => combat.GetSide(source.Side).GetAllCharacters(),
            ActionCardTarget.FrontOpponent => new[]
            {
                combat.GetSide(source.Side.OtherSide()).GetCharacter(CombatPosition.Front)
            },
            ActionCardTarget.BackOpponent => new[]
            {
                combat.GetSide(source.Side.OtherSide()).GetCharacter(CombatPosition.Back)
                ?? combat.GetSide(source.Side.OtherSide()).GetCharacter(CombatPosition.Front)
            },
            ActionCardTarget.AllOpponents => combat.GetSide(source.Side.OtherSide()).GetAllCharacters(),
            ActionCardTarget.FrontAlly => new[] { combat.GetSide(source.Side).GetCharacter(CombatPosition.Front) },
            ActionCardTarget.BackAlly => new[]
                { combat.GetSide(source.Side).GetCharacter(CombatPosition.Back) ?? combat.GetSide(source.Side).GetCharacter(CombatPosition.Front) },
            ActionCardTarget.AllAllies => combat.GetSide(source.Side).GetAllCharacters(),
            _ => throw new ArgumentOutOfRangeException(nameof(target), target, null)
        };

        return characters.Where(c => c != null).Select(c => c!);
    }
}
