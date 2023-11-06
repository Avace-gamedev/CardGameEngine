using System.Runtime.CompilerServices;
using CardGame.Engine.Characters;
using CardGame.Engine.Combats;
using CardGame.Engine.Combats.Ai;
using CardGame.Engine.Combats.State;
using MediatR;
using PockedeckBattler.Server.GameContent.Characters;
using PockedeckBattler.Server.Rest.Combats.Exceptions;
using PockedeckBattler.Server.Rest.Combats.Notifications;
using PockedeckBattler.Server.Stores;
using PockedeckBattler.Server.Stores.Combats;
using PockedeckBattler.Server.Stores.CombatsInPreparation;

namespace PockedeckBattler.Server.Rest.Combats;

public class CombatsService : ICombatService
{
    readonly IMediator _mediator;

    // cache combat instances because we use register to their event handlers
    readonly Dictionary<Guid, CombatInstanceWithMetadata> _registered = new();

    readonly IStore<CombatInstanceWithMetadata> _store;

    public CombatsService(IStore<CombatInstanceWithMetadata> store, IMediator mediator)
    {
        _store = store;
        _mediator = mediator;
    }

    public async Task<CombatInstanceWithMetadata> CreateCombat(CombatInPreparation config, CancellationToken cancellationToken = default)
    {
        if (config.LeftFrontCharacter == null && config.LeftBackCharacter == null)
        {
            throw new InvalidCombatConfigurationException("Left side doesn't have any character");
        }

        if (config.RightPlayerName == null)
        {
            throw new InvalidCombatConfigurationException("Right side not configured");
        }

        if (config.RightFrontCharacter == null && config.RightBackCharacter == null)
        {
            throw new InvalidCombatConfigurationException("Right side doesn't have any character");
        }

        if (!config.LeftReady)
        {
            throw new InvalidCombatConfigurationException("Left side not ready");
        }

        if (!config.RightReady)
        {
            throw new InvalidCombatConfigurationException("Right side not ready");
        }

        Character? leftFrontCharacter = config.LeftFrontCharacter == null ? null : Characters.RequireByName(config.LeftFrontCharacter);
        Character? leftBackCharacter = config.LeftBackCharacter == null ? null : Characters.RequireByName(config.LeftBackCharacter);

        Character? rightFrontCharacter = config.RightFrontCharacter == null ? null : Characters.RequireByName(config.RightFrontCharacter);
        Character? rightBackCharacter = config.RightBackCharacter == null ? null : Characters.RequireByName(config.RightBackCharacter);

        CombatState combatState = new(
            new[] { leftFrontCharacter, leftBackCharacter }.Where(c => c != null).Select(c => c!).ToArray(),
            new[] { rightFrontCharacter, rightBackCharacter }.Where(c => c != null).Select(c => c!).ToArray(),
            string.IsNullOrWhiteSpace(config.RandomSeed) ? null : config.RandomSeed.GetHashCode()
        );

        CombatInstance combatInstance = new(combatState, new CombatOptions());

        if (config.RightPlayerIsAi)
        {
            combatInstance.SetAi(CombatSide.Right, new CombatAiOptions(Random.Shared.Next()));
        }

        CombatInstanceWithMetadata combat = new(config.Id, config.LeftPlayerName, config.RightPlayerName, combatInstance, config);
        await SaveCombat(combat, cancellationToken);

        Register(combat);

        await _mediator.Publish(new CombatNotification(combat, CombatEvent.Created), cancellationToken);

        return combat;
    }

    public async Task SaveCombat(CombatInstanceWithMetadata combat, CancellationToken cancellationToken = default)
    {
        await _store.Save(GetKey(combat.Id), combat, cancellationToken);
    }

    public async Task<CombatInstanceWithMetadata?> GetCombat(Guid id, CancellationToken cancellationToken = default)
    {
        if (_registered.TryGetValue(id, out CombatInstanceWithMetadata? combat))
        {
            return combat;
        }

        combat = await _store.Load(GetKey(id), cancellationToken);

        if (combat != null)
        {
            Register(combat);
        }

        return combat;
    }

    public async IAsyncEnumerable<CombatInstanceWithMetadata> GetCombatsInvolvingPlayer(
        string player,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    )
    {
        await foreach (CombatInstanceWithMetadata combat in GetAll(cancellationToken))
        {
            if (combat.LeftPlayerName == player || combat.RightPlayerName == player)
            {
                yield return combat;
            }
        }
    }

    /// <summary>
    ///     Return all combats using both the cached values in <see cref="_registered" /> and the new ones from <see cref="_store" />
    /// </summary>
    async IAsyncEnumerable<CombatInstanceWithMetadata> GetAll([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        foreach (CombatInstanceWithMetadata combat in _registered.Values)
        {
            yield return combat;
        }

        await foreach (CombatInstanceWithMetadata combat in _store.LoadAll(cancellationToken))
        {
            if (IsRegistered(combat))
            {
                continue;
            }

            Register(combat);
            yield return combat;
        }
    }

    bool IsRegistered(CombatInstanceWithMetadata combat)
    {
        return _registered.ContainsKey(combat.Id);
    }

    void Register(CombatInstanceWithMetadata combat)
    {
        _registered[combat.Id] = combat;

        combat.Instance.State.TurnStarted += (_, _) => _mediator.Publish(new CombatNotification(combat, CombatEvent.Updated));
        combat.Instance.State.TurnEnded += (_, _) => _mediator.Publish(new CombatNotification(combat, CombatEvent.Updated));
        combat.Instance.State.PhaseStarted += (_, _) => _mediator.Publish(new CombatNotification(combat, CombatEvent.Updated));
        combat.Instance.State.PhaseEnded += (_, _) => _mediator.Publish(new CombatNotification(combat, CombatEvent.Updated));
        combat.Instance.State.Ended += (_, _) => _mediator.Publish(new CombatNotification(combat, CombatEvent.Ended));

        combat.Instance.State.LeftSide.CardDrawn += (_, _) => _mediator.Publish(new CombatNotification(combat, CombatEvent.Updated));
        combat.Instance.State.LeftSide.CardReturned += (_, _) => _mediator.Publish(new CombatNotification(combat, CombatEvent.Updated));
        combat.Instance.State.LeftSide.CardDiscarded += (_, _) => _mediator.Publish(new CombatNotification(combat, CombatEvent.Updated));
        combat.Instance.State.LeftSide.DeckShuffled += (_, _) => _mediator.Publish(new CombatNotification(combat, CombatEvent.Updated));
        combat.Instance.State.LeftSide.CharacterPositionChanged += (_, _) => _mediator.Publish(new CombatNotification(combat, CombatEvent.Updated));
        combat.Instance.State.LeftSide.CharacterRemoved += (_, _) => _mediator.Publish(new CombatNotification(combat, CombatEvent.Updated));
        combat.Instance.State.LeftSide.ApChanged += (_, _) => _mediator.Publish(new CombatNotification(combat, CombatEvent.Updated));

        combat.Instance.State.RightSide.CardDrawn += (_, _) => _mediator.Publish(new CombatNotification(combat, CombatEvent.Updated));
        combat.Instance.State.RightSide.CardReturned += (_, _) => _mediator.Publish(new CombatNotification(combat, CombatEvent.Updated));
        combat.Instance.State.RightSide.CardDiscarded += (_, _) => _mediator.Publish(new CombatNotification(combat, CombatEvent.Updated));
        combat.Instance.State.RightSide.DeckShuffled += (_, _) => _mediator.Publish(new CombatNotification(combat, CombatEvent.Updated));
        combat.Instance.State.RightSide.CharacterPositionChanged += (_, _) => _mediator.Publish(new CombatNotification(combat, CombatEvent.Updated));
        combat.Instance.State.RightSide.CharacterRemoved += (_, _) => _mediator.Publish(new CombatNotification(combat, CombatEvent.Updated));
        combat.Instance.State.RightSide.ApChanged += (_, _) => _mediator.Publish(new CombatNotification(combat, CombatEvent.Updated));
    }

    static string GetKey(Guid id)
    {
        return id.ToString();
    }
}
