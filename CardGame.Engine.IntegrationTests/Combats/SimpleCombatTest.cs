using CardGame.Engine.Cards.ActionCard;
using CardGame.Engine.Characters;
using CardGame.Engine.Combats;
using CardGame.Engine.Combats.Exceptions;
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

        _combat = new CombatInstance(
            new[] { _leftCharacter },
            new[] { _rightCharacter },
            new CombatOptions { HandSize = 2, StartingAp = 4, StartingSide = CombatSide.Left }
        );
    }

    [TestMethod]
    public void ShouldStartCombat()
    {
        _combat.Ongoing.Should().BeFalse();

        _combat.Start();

        _combat.Ongoing.Should().BeTrue();
        _combat.Over.Should().BeFalse();
        _combat.Turn.Should().Be(1);
        _combat.Side.Should().Be(CombatSide.Left);
        _combat.Phase.Should().Be(CombatSideTurnPhase.Play);

        _combat.CurrentSide.Side.Should().Be(CombatSide.Left);
        _combat.CurrentSide.Front.Character.Should().Be(_leftCharacter);
        _combat.CurrentSide.Front.Health.Should().Be(9);
        _combat.CurrentSide.Back.Should().BeNull();
        _combat.CurrentSide.Ap.Should().Be(4);
        _combat.CurrentSide.NoMovesLeft.Should().BeFalse();
        _combat.CurrentSide.Lost.Should().BeFalse();
        _combat.CurrentSide.Hand.Should()
            .Satisfy(c => c.Card == _leftCard1 && c.Character.Character == _leftCharacter, c => c.Card == _leftCard2 && c.Character.Character == _leftCharacter);
        _combat.CurrentSide.Deck.Should().BeEmpty();

        _combat.OtherSide.Side.Should().Be(CombatSide.Right);
        _combat.OtherSide.Front.Character.Should().Be(_rightCharacter);
        _combat.OtherSide.Front.Health.Should().Be(10);
        _combat.OtherSide.Back.Should().BeNull();
        _combat.OtherSide.Ap.Should().Be(0);
        _combat.OtherSide.Lost.Should().BeFalse();
        _combat.OtherSide.Hand.Should().BeEmpty();
        _combat.OtherSide.Deck.Should().Satisfy(c => c.Card == _rightCard && c.Character.Character == _rightCharacter);
    }

    [TestMethod]
    public void ShouldPlayCard()
    {
        _combat.Start();
        _combat.PlayCardAt(CombatSide.Left, 0);

        _combat.CurrentSide.Ap.Should().Be(1);
        _combat.CurrentSide.NoMovesLeft.Should().BeTrue();
        _combat.OtherSide.Front.Health.Should().Be(5);
    }

    [TestMethod]
    public void ShouldEndSideTurn()
    {
        _combat.Start();
        _combat.EndCurrentSideTurnAndStartNextOne();

        _combat.Ongoing.Should().BeTrue();
        _combat.Over.Should().BeFalse();
        _combat.Turn.Should().Be(1);
        _combat.Side.Should().Be(CombatSide.Right);
        _combat.Phase.Should().Be(CombatSideTurnPhase.Play);

        _combat.CurrentSide.Side.Should().Be(CombatSide.Right);
        _combat.CurrentSide.Front.Character.Should().Be(_rightCharacter);
        _combat.CurrentSide.Front.Health.Should().Be(10);
        _combat.CurrentSide.Back.Should().BeNull();
        _combat.CurrentSide.Ap.Should().Be(4);
        _combat.CurrentSide.Lost.Should().BeFalse();
        _combat.CurrentSide.Hand.Should().Satisfy(c => c.Card == _rightCard && c.Character.Character == _rightCharacter);
        _combat.CurrentSide.Deck.Should().BeEmpty();

        _combat.OtherSide.Side.Should().Be(CombatSide.Left);
        _combat.OtherSide.Front.Character.Should().Be(_leftCharacter);
        _combat.OtherSide.Front.Health.Should().Be(9);
        _combat.OtherSide.Back.Should().BeNull();
        _combat.OtherSide.Ap.Should().Be(4);
        _combat.OtherSide.NoMovesLeft.Should().BeFalse();
        _combat.OtherSide.Lost.Should().BeFalse();
        _combat.OtherSide.Hand.Should()
            .Satisfy(c => c.Card == _leftCard1 && c.Character.Character == _leftCharacter, c => c.Card == _leftCard2 && c.Character.Character == _leftCharacter);
        _combat.OtherSide.Deck.Should().BeEmpty();
    }

    [TestMethod]
    public void ShouldEndTurn()
    {
        _combat.Start();
        _combat.EndCurrentSideTurnAndStartNextOne();
        _combat.EndCurrentSideTurnAndStartNextOne();

        _combat.Ongoing.Should().BeTrue();
        _combat.Over.Should().BeFalse();
        _combat.Turn.Should().Be(2);
        _combat.Side.Should().Be(CombatSide.Left);
        _combat.Phase.Should().Be(CombatSideTurnPhase.Play);

        _combat.CurrentSide.Side.Should().Be(CombatSide.Left);
        _combat.CurrentSide.Front.Character.Should().Be(_leftCharacter);
        _combat.CurrentSide.Front.Health.Should().Be(9);
        _combat.CurrentSide.Back.Should().BeNull();
        _combat.CurrentSide.Ap.Should().Be(5);
        _combat.CurrentSide.NoMovesLeft.Should().BeFalse();
        _combat.CurrentSide.Lost.Should().BeFalse();
        _combat.CurrentSide.Hand.Should()
            .Satisfy(c => c.Card == _leftCard1 && c.Character.Character == _leftCharacter, c => c.Card == _leftCard2 && c.Character.Character == _leftCharacter);
        _combat.CurrentSide.Deck.Should().BeEmpty();

        _combat.OtherSide.Side.Should().Be(CombatSide.Right);
        _combat.OtherSide.Front.Character.Should().Be(_rightCharacter);
        _combat.OtherSide.Front.Health.Should().Be(10);
        _combat.OtherSide.Back.Should().BeNull();
        _combat.OtherSide.Ap.Should().Be(4);
        _combat.OtherSide.Lost.Should().BeFalse();
        _combat.OtherSide.Hand.Should().Satisfy(c => c.Card == _rightCard && c.Character.Character == _rightCharacter);
        _combat.OtherSide.Deck.Should().BeEmpty();
    }

    [TestMethod]
    public void ShouldKillOpponentAndEndCombat()
    {
        _combat.Start();
        _combat.PlayCardAt(CombatSide.Left, 0);
        _combat.EndCurrentSideTurnAndStartNextOne();
        _combat.EndCurrentSideTurnAndStartNextOne();
        _combat.PlayCardAt(CombatSide.Left, 0);

        _combat.Ongoing.Should().BeFalse();
        _combat.Over.Should().BeTrue();
        _combat.Winner.Should().Be(CombatSide.Left);
    }

    [TestMethod]
    public void ShouldNotPlayCardIfNotEnoughAp()
    {
        _combat.Start();
        _combat.PlayCardAt(CombatSide.Left, 0);

        Action playCardWithoutEnoughAp = () => _combat.PlayCardAt(CombatSide.Left, 0);

        playCardWithoutEnoughAp.Should().Throw<InvalidMoveException>();
    }

    [TestMethod]
    public void ShouldNotPlayNonExistingCard()
    {
        _combat.Start();

        Action playCardOutOfRange = () => _combat.PlayCardAt(CombatSide.Left, 5);

        playCardOutOfRange.Should().Throw<InvalidMoveException>();
    }

    [TestMethod]
    public void ShouldNotPlayCardIfNotYourTurn()
    {
        _combat.Start();

        Action playCardOutOfRange = () => _combat.PlayCardAt(CombatSide.Right, 0);

        playCardOutOfRange.Should().Throw<InvalidMoveException>();
    }
}
