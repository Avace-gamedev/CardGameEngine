using CardGame.Engine.Cards.ActionCard;

namespace CardGame.Engine.Characters;

public class Character
{
    readonly List<ActionCard> _deck;

    public Character(CharacterIdentity identity, CharacterStatistics baseStats, IEnumerable<ActionCard> deck)
    {
        Identity = identity;
        Stats = baseStats;
        _deck = deck.ToList();
    }


    public CharacterIdentity Identity { get; }
    public CharacterStatistics Stats { get; }
    public IReadOnlyCollection<ActionCard> Deck => _deck;
}
