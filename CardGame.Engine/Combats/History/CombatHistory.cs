namespace CardGame.Engine.Combats.History;

public class CombatHistory
{
    readonly List<CombatAction> _actions;

    public CombatHistory(InitialCombatState initialState)
    {
        InitialState = initialState;
        _actions = new List<CombatAction>();
    }

    public InitialCombatState InitialState { get; set; }
    public IReadOnlyList<CombatAction> Actions => _actions;

    public void RecordCardPlay(CombatSide side, int index)
    {
        _actions.Add(new PlayCardAction(side, index));
    }

    public void RecordEndTurn(CombatSide side)
    {
        _actions.Add(new EndTurnAction(side));
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

        return instance;
    }
}
