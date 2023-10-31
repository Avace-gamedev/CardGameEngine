using CardGame.Engine.Cards.ActionCard;
using CardGame.Engine.Characters;

namespace PockedeckBattler.Views;

public class CharacterView
{
    public CharacterView(CharacterIdentity identity, CharacterStatistics statistics, ActionCard[] deck)
    {
        Identity = identity;
        Statistics = statistics;
        Deck = deck;
    }

    public CharacterIdentity Identity { get; }
    public CharacterStatistics Statistics { get; }
    public ActionCard[] Deck { get; }
}

public static class CharacterViewMappingExtensions
{
    public static CharacterView View(this Character character)
    {
        return new CharacterView(character.Identity, character.Stats, character.Deck.ToArray());
    }
}
