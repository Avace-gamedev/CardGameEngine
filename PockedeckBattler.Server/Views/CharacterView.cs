using System.ComponentModel.DataAnnotations;
using CardGame.Engine.Characters;

namespace PockedeckBattler.Server.Views;

public class CharacterView
{
    public CharacterView(CharacterIdentity identity, CharacterStatistics statistics, ActionCardView[] deck)
    {
        Identity = identity;
        Statistics = statistics;
        Deck = deck;
    }

    [Required]
    public CharacterIdentity Identity { get; }

    [Required]
    public CharacterStatistics Statistics { get; }

    [Required]
    public ActionCardView[] Deck { get; }
}

public static class CharacterViewMappingExtensions
{
    public static CharacterView View(this Character character)
    {
        return new CharacterView(character.Identity, character.Stats, character.Deck.Select(c => c.View()).ToArray());
    }
}
