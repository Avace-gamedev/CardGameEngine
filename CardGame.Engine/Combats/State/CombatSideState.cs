using CardGame.Engine.Characters;
using CardGame.Engine.Combats.Cards;
using CardGame.Engine.Combats.Exceptions;
using CardGame.Engine.Extensions;

namespace CardGame.Engine.Combats.State;

public class CombatSideState
{
    readonly List<ActionCardInstance> _deck;
    readonly List<ActionCardInstance> _hand;

    public CombatSideState(CombatState combat, CombatSide side, IReadOnlyList<Character> characters)
    {
        Side = side;

        Character? backLeftCharacter = characters.ElementAtOrDefault(1);
        CharacterCombatState front = new(combat, side, characters[0]);
        CharacterCombatState? back = backLeftCharacter == null ? null : new CharacterCombatState(combat, side, backLeftCharacter);

        Front = front;
        Back = back;

        _hand = new List<ActionCardInstance>();

        _deck = new List<ActionCardInstance>();
        _deck.AddRange(front.Character.Deck.Select(c => new ActionCardInstance(c, front)));
        if (back != null)
        {
            _deck.AddRange(back.Character.Deck.Select(c => new ActionCardInstance(c, back)));
        }

    }

    public CombatSide Side { get; }
    public int Ap { get; private set; }

    public CharacterCombatState? Front { get; private set; }
    public CharacterCombatState? Back { get; private set; }

    public IReadOnlyList<ActionCardInstance> Hand => _hand;
    public IReadOnlyList<ActionCardInstance> Deck => _deck;

    public event EventHandler<ActionCardInstance>? CardDrawn;
    public event EventHandler<ActionCardInstance>? CardReturned;
    public event EventHandler<CardDiscardedEventArgs>? CardDiscarded;
    public event EventHandler? DeckShuffled;

    public event EventHandler? CharacterPositionChanged;
    public event EventHandler<CharacterCombatState>? CharacterRemoved;

    public event EventHandler<ApChangedEventArgs>? ApChanged;

    public CharacterCombatState? GetCharacter(CombatPosition position)
    {
        return position switch
        {
            CombatPosition.Front => Front,
            CombatPosition.Back => Back,
            _ => throw new ArgumentOutOfRangeException(nameof(position), position, null)
        };
    }

    public IEnumerable<CharacterCombatState> GetAllCharacters()
    {
        return new[] { Front, Back }.Where(c => c != null).Select(c => c!);
    }

    internal bool DrawRandomCardFromDeckToHand()
    {
        ActionCardInstance? card = _deck.FirstOrDefault();
        if (card == null)
        {
            return false;
        }

        _hand.Add(card);
        _deck.RemoveAt(0);

        CardDrawn?.Invoke(this, card);

        return true;
    }

    internal void ReturnCardFromHandToDeck(int index)
    {
        ActionCardInstance? card = _hand.ElementAtOrDefault(index);
        if (card == null)
        {
            throw new Exception("Card not found");
        }

        _hand.RemoveAt(index);

        int randomPosition = Random.Shared.Next(0, _deck.Count + 1);
        _deck.Insert(randomPosition, card);

        CardReturned?.Invoke(this, card);
    }

    internal void SwapCharacters()
    {
        if (Front == null || Back == null)
        {
            return;
        }

        (Front, Back) = (Back, Front);

        CharacterPositionChanged?.Invoke(this, EventArgs.Empty);
    }

    internal void RemoveCharacterAndItsCards(CombatPosition position)
    {
        switch (position)
        {
            case CombatPosition.Front:
                if (Front == null)
                {
                    return;
                }

                CharacterCombatState? front = Front;
                Front = null;

                DiscardCardsOfCharacter(front.Character);
                CharacterRemoved?.Invoke(this, front);

                if (Back != null)
                {
                    Front = Back;
                    Back = null;

                    CharacterPositionChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case CombatPosition.Back:
                if (Back == null)
                {
                    return;
                }

                CharacterCombatState? back = Back;
                Back = null;

                DiscardCardsOfCharacter(back.Character);
                CharacterRemoved?.Invoke(this, back);

                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(position), position, null);
        }
    }

    internal void ShuffleDeck()
    {
        Random.Shared.Shuffle(_deck);

        DeckShuffled?.Invoke(this, EventArgs.Empty);
    }

    internal void ConsumeAp(int ap)
    {
        if (ap <= 0)
        {
            return;
        }

        if (ap > Ap)
        {
            throw new InvalidMoveException("Not enough Ap");
        }

        int oldAp = Ap;
        Ap -= ap;

        ApChanged?.Invoke(this, new ApChangedEventArgs(oldAp, Ap));
    }

    internal void SetAp(int ap)
    {
        int oldAp = Ap;
        Ap = ap;

        ApChanged?.Invoke(this, new ApChangedEventArgs(oldAp, Ap));
    }

    void DiscardCardsOfCharacter(Character character)
    {
        foreach (ActionCardInstance card in _hand.ToArray())
        {
            if (card.Character.Character.Identity.Name == character.Identity.Name)
            {
                _hand.Remove(card);

                CardDiscarded?.Invoke(this, new CardDiscardedEventArgs(card, CardLocation.Hand));
            }
        }

        foreach (ActionCardInstance card in _deck.ToArray())
        {
            if (card.Character.Character.Identity.Name == character.Identity.Name)
            {
                _hand.Remove(card);

                CardDiscarded?.Invoke(this, new CardDiscardedEventArgs(card, CardLocation.Deck));
            }
        }
    }
}

public class CardDiscardedEventArgs
{
    public CardDiscardedEventArgs(ActionCardInstance card, CardLocation from)
    {
        Card = card;
        From = from;
    }

    public ActionCardInstance Card { get; }
    public CardLocation From { get; }
}

public class ApChangedEventArgs
{
    public ApChangedEventArgs(int oldAp, int newAp)
    {
        OldAp = oldAp;
        NewAp = newAp;
    }

    public int OldAp { get; }
    public int NewAp { get; }
}
