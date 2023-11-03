using CardGame.Engine.Cards.ActionCard;
using CardGame.Engine.Characters;
using CardGame.Engine.Combats;
using CardGame.Engine.Combats.Exceptions;
using CardGame.Engine.Combats.State;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CardGame.Engine.Tests.Combats;

[TestClass]
public class SimpleCombatTest
{
    CombatInstance _combat = null!;
    ActionCard _leftCard1 = null!;
    ActionCard _leftCard2 = null!;
    Character _leftCharacter = null!;
    ActionCard _rightCard = null!;
    Character _rightCharacter = null!;

    [TestInitialize]
    public void Initialize()
    {
        _leftCard1 = ActionCard.Damage("Damage Card 1", 3, ActionCardTarget.FrontOpponent, 5, Element.Neutral);
        _leftCard2 = ActionCard.Damage("Damage Card 2", 3, ActionCardTarget.FrontOpponent, 5, Element.Neutral);
        _leftCharacter = new Character(new CharacterIdentity("left", "Left"), new CharacterStatistics { MaxHealth = 9 }, new[] { _leftCard1, _leftCard2 });

        _rightCard = ActionCard.Damage("Some Card", 3, ActionCardTarget.FrontOpponent, 5, Element.Neutral);
        _rightCharacter = new Character(new CharacterIdentity("right", "Right"), new CharacterStatistics { MaxHealth = 10 }, new[] { _rightCard });

        CombatState state = CombatState.Create(new[] { _leftCharacter }, new[] { _rightCharacter });

        _combat = new CombatInstance(state, new CombatOptions { HandSizeWithBothCharacters = 2, StartingAp = 4, StartingSide = CombatSide.Left });
    }

    [TestMethod]
    public void ShouldStartCombat()
    {
        _combat.State.Ongoing.Should().BeTrue();
        _combat.State.Over.Should().BeFalse();
        _combat.State.Turn.Should().Be(1);
        _combat.State.Side.Should().Be(CombatSide.Left);
        _combat.State.Phase.Should().Be(CombatSideTurnPhase.Play);

        _combat.State.CurrentSide.Side.Should().Be(CombatSide.Left);
        _combat.State.CurrentSide.Front.Should().NotBeNull();
        _combat.State.CurrentSide.Front!.Character.Should().Be(_leftCharacter);
        _combat.State.CurrentSide.Front.Health.Should().Be(9);
        _combat.State.CurrentSide.Back.Should().BeNull();
        _combat.State.CurrentSide.Ap.Should().Be(4);
        _combat.State.CurrentSide.Hand.Should()
            .Satisfy(c => c.Card == _leftCard1 && c.Character.Character == _leftCharacter, c => c.Card == _leftCard2 && c.Character.Character == _leftCharacter);
        _combat.State.CurrentSide.Deck.Should().BeEmpty();

        _combat.State.OtherSide.Side.Should().Be(CombatSide.Right);
        _combat.State.OtherSide.Front.Should().NotBeNull();
        _combat.State.OtherSide.Front!.Character.Should().Be(_rightCharacter);
        _combat.State.OtherSide.Front.Health.Should().Be(10);
        _combat.State.OtherSide.Back.Should().BeNull();
        _combat.State.OtherSide.Ap.Should().Be(0);
        _combat.State.OtherSide.Hand.Should().BeEmpty();
        _combat.State.OtherSide.Deck.Should().Satisfy(c => c.Card == _rightCard && c.Character.Character == _rightCharacter);
    }

    [TestMethod]
    public void ShouldPlayCard()
    {
        _combat.PlayCard(CombatSide.Left, 0);

        _combat.State.CurrentSide.Ap.Should().Be(1);
        _combat.State.OtherSide.Front.Should().NotBeNull();
        _combat.State.OtherSide.Front!.Health.Should().Be(5);
    }

    [TestMethod]
    public void ShouldEndSideTurn()
    {
        _combat.EndTurn(CombatSide.Left);

        _combat.State.Ongoing.Should().BeTrue();
        _combat.State.Over.Should().BeFalse();
        _combat.State.Turn.Should().Be(1);
        _combat.State.Side.Should().Be(CombatSide.Right);
        _combat.State.Phase.Should().Be(CombatSideTurnPhase.Play);

        _combat.State.CurrentSide.Side.Should().Be(CombatSide.Right);
        _combat.State.CurrentSide.Front.Should().NotBeNull();
        _combat.State.CurrentSide.Front!.Character.Should().Be(_rightCharacter);
        _combat.State.CurrentSide.Front.Health.Should().Be(10);
        _combat.State.CurrentSide.Back.Should().BeNull();
        _combat.State.CurrentSide.Ap.Should().Be(4);
        _combat.State.CurrentSide.Hand.Should().Satisfy(c => c.Card == _rightCard && c.Character.Character == _rightCharacter);
        _combat.State.CurrentSide.Deck.Should().BeEmpty();

        _combat.State.OtherSide.Side.Should().Be(CombatSide.Left);
        _combat.State.OtherSide.Front.Should().NotBeNull();
        _combat.State.OtherSide.Front!.Character.Should().Be(_leftCharacter);
        _combat.State.OtherSide.Front.Health.Should().Be(9);
        _combat.State.OtherSide.Back.Should().BeNull();
        _combat.State.OtherSide.Ap.Should().Be(4);
        _combat.State.OtherSide.Hand.Should()
            .Satisfy(c => c.Card == _leftCard1 && c.Character.Character == _leftCharacter, c => c.Card == _leftCard2 && c.Character.Character == _leftCharacter);
        _combat.State.OtherSide.Deck.Should().BeEmpty();
    }

    [TestMethod]
    public void ShouldEndTurn()
    {
        _combat.EndTurn(CombatSide.Left);
        _combat.EndTurn(CombatSide.Right);

        _combat.State.Ongoing.Should().BeTrue();
        _combat.State.Over.Should().BeFalse();
        _combat.State.Turn.Should().Be(2);
        _combat.State.Side.Should().Be(CombatSide.Left);
        _combat.State.Phase.Should().Be(CombatSideTurnPhase.Play);

        _combat.State.CurrentSide.Side.Should().Be(CombatSide.Left);
        _combat.State.CurrentSide.Front.Should().NotBeNull();
        _combat.State.CurrentSide.Front!.Character.Should().Be(_leftCharacter);
        _combat.State.CurrentSide.Front.Health.Should().Be(9);
        _combat.State.CurrentSide.Back.Should().BeNull();
        _combat.State.CurrentSide.Ap.Should().Be(5);
        _combat.State.CurrentSide.Hand.Should()
            .Satisfy(c => c.Card == _leftCard1 && c.Character.Character == _leftCharacter, c => c.Card == _leftCard2 && c.Character.Character == _leftCharacter);
        _combat.State.CurrentSide.Deck.Should().BeEmpty();

        _combat.State.OtherSide.Side.Should().Be(CombatSide.Right);
        _combat.State.OtherSide.Front.Should().NotBeNull();
        _combat.State.OtherSide.Front!.Character.Should().Be(_rightCharacter);
        _combat.State.OtherSide.Front.Health.Should().Be(10);
        _combat.State.OtherSide.Back.Should().BeNull();
        _combat.State.OtherSide.Ap.Should().Be(4);
        _combat.State.OtherSide.Hand.Should().Satisfy(c => c.Card == _rightCard && c.Character.Character == _rightCharacter);
        _combat.State.OtherSide.Deck.Should().BeEmpty();
    }

    [TestMethod]
    public void ShouldKillOpponentAndEndCombat()
    {
        _combat.PlayCard(CombatSide.Left, 0);
        _combat.EndTurn(CombatSide.Left);
        _combat.EndTurn(CombatSide.Right);
        _combat.PlayCard(CombatSide.Left, 0);

        _combat.State.Ongoing.Should().BeFalse();
        _combat.State.Over.Should().BeTrue();
        _combat.State.Winner.Should().Be(CombatSide.Left);
    }

    [TestMethod]
    public void ShouldNotPlayCardIfNotEnoughAp()
    {
        _combat.PlayCard(CombatSide.Left, 0);

        Action playCardWithoutEnoughAp = () => _combat.PlayCard(CombatSide.Left, 0);

        playCardWithoutEnoughAp.Should().Throw<InvalidMoveException>();
    }

    [TestMethod]
    public void ShouldNotPlayNonExistingCard()
    {
        Action playCardOutOfRange = () => _combat.PlayCard(CombatSide.Left, 5);

        playCardOutOfRange.Should().Throw<InvalidMoveException>();
    }

    [TestMethod]
    public void ShouldNotPlayCardIfNotYourTurn()
    {
        Action playCardOutOfRange = () => _combat.PlayCard(CombatSide.Right, 0);

        playCardOutOfRange.Should().Throw<InvalidMoveException>();
    }
}
