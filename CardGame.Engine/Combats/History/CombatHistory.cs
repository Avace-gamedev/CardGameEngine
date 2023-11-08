using CardGame.Engine.Combats.Abstractions;
using CardGame.Engine.Combats.Ai;

namespace CardGame.Engine.Combats.History;

public class CombatHistory
{
    readonly List<CombatAction> _actions;

    public CombatHistory(
        InitialCombatState initialState,
        IReadOnlyList<CombatAction>? actions = null,
        CombatAiOptions? leftAiOptions = null,
        CombatAiOptions? rightAiOptions = null
    )
    {
        InitialState = initialState;
        LeftAiOptions = leftAiOptions;
        RightAiOptions = rightAiOptions;
        _actions = actions?.ToList() ?? new List<CombatAction>();
    }

    public InitialCombatState InitialState { get; }
    public CombatAiOptions? LeftAiOptions { get; private set; }
    public CombatAiOptions? RightAiOptions { get; private set; }
    public IReadOnlyList<CombatAction> Actions => _actions;

    public void RecordCardPlay(CombatSide side, int index)
    {
        _actions.Add(new PlayCardAction(side, index));
    }

    public void RecordEndTurn(CombatSide side)
    {
        _actions.Add(new EndTurnAction(side));
    }

    public void RecordSetAi(CombatSide side, CombatAiOptions options)
    {
        switch (side)
        {
            case CombatSide.Left:
                LeftAiOptions = options;
                break;
            case CombatSide.Right:
                RightAiOptions = options;
                break;
            case CombatSide.None:
            default:
                throw new ArgumentOutOfRangeException(nameof(side), side, null);
        }
    }

    public CombatInstance Replay(int frame = -1)
    {
        CombatInstance instance = new(InitialState.LeftCharacters, InitialState.RightCharacters, InitialState.Options);

        IEnumerable<CombatAction> actionsToPlay = frame >= 0 ? Actions.Take(frame) : Actions;

        foreach (CombatAction action in actionsToPlay)
        {
            switch (action)
            {
                case PlayCardAction playCardAction:
                    instance.PlayCard(playCardAction.Side, playCardAction.CardIndex);
                    break;
                case EndTurnAction endTurnAction:
                    instance.EndTurn(endTurnAction.Side);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action));
            }
        }

        if (LeftAiOptions != null)
        {
            instance.SetAi(CombatSide.Left, LeftAiOptions);
        }

        if (RightAiOptions != null)
        {
            instance.SetAi(CombatSide.Right, RightAiOptions);
        }

        return instance;
    }
}
