using CardGame.Engine.Combats.Abstractions;
using CardGame.Engine.Combats.Cards;
using CardGame.Engine.Combats.Characters;
using CardGame.Engine.Effects.Enchantments;
using CardGame.Engine.Effects.Enchantments.Triggered.Instance;

namespace CardGame.Engine.Combats.Logs;

public class CombatLog
{
    readonly List<CombatTurn> _turns = new();
    public IReadOnlyList<CombatTurn> Turns => _turns;

    public void RecordPhaseChange(int turn, CombatSide side, CombatSideTurnPhase phase)
    {
        CombatTurn? lastTurn = _turns.LastOrDefault();
        if (lastTurn == null || lastTurn.Turn != turn)
        {
            _turns.Add(new CombatTurn(turn));
        }

        CombatTurn currentTurn = _turns.Last();
        currentTurn.Add(new TurnPhase(side, phase));
    }

    public IDisposable RecordEffectsOfPlayingCard(ActionCardInstance card)
    {
        CharacterCombatState source = card.Character;
        return new EffectsOnCharacterRecorder(
            source.Combat,
            effects =>
            {
                CardPlayedLogEntry characterEntry = new(
                    new CharacterLogEntry(source.Character.Identity.Name, source.Side),
                    card.GetCardWithModifications(),
                    effects.ToArray()
                );

                _turns.LastOrDefault()?.Add(characterEntry);
            }
        );
    }

    public IDisposable RecordTriggeredEffect(TriggeredEffectInstance instance)
    {
        return new EffectsOnCharacterRecorder(
            instance.Source.Combat,
            effects =>
            {
                TriggeredEffectLogEntry entry = new(
                    new CharacterLogEntry(instance.Source.Character.Identity.Name, instance.Source.Side),
                    new CharacterLogEntry(instance.Target.Character.Identity.Name, instance.Target.Side),
                    instance.Enchantment.Enchantment,
                    instance.Effect,
                    effects.ToArray()
                );

                _turns.LastOrDefault()?.Add(entry);
            }
        );
    }

    public void RecordEnchantmentExpired(EnchantmentInstance instance)
    {
        _turns.LastOrDefault()
            ?.Add(
                new EnchantmentExpiredLogEntry(
                    new CharacterLogEntry(instance.Source.Character.Identity.Name, instance.Source.Side),
                    new CharacterLogEntry(instance.Target.Character.Identity.Name, instance.Target.Side),
                    instance.Enchantment
                )
            );
    }

    public void RecordEnd(CombatSide winner)
    {
        _turns.LastOrDefault()?.Add(new CombatEndedLogEntry(winner));
    }

    public class CombatTurn
    {
        readonly List<TurnPhase> _phases = new();

        public CombatTurn(int turn)
        {
            Turn = turn;
        }

        public int Turn { get; }
        public IReadOnlyList<TurnPhase> Phases => _phases;

        internal void Add(TurnPhase phase)
        {
            _phases.Add(phase);
        }

        internal void Add(CombatLogEntry entry)
        {
            TurnPhase? currentPhase = _phases.LastOrDefault();
            currentPhase?.Add(entry);
        }
    }

    public class TurnPhase
    {
        readonly List<CombatLogEntry> _entries = new();

        public TurnPhase(CombatSide side, CombatSideTurnPhase phase)
        {
            Phase = phase;
            Side = side;
        }

        public CombatSide Side { get; }
        public CombatSideTurnPhase Phase { get; }
        public IReadOnlyList<CombatLogEntry> Entries => _entries;

        internal void Add(CombatLogEntry entry)
        {
            _entries.Add(entry);
        }
    }
}
