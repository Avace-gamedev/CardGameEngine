namespace PockedeckBattler.Server.SignalR.Combats;

public class CombatsHub : BaseHub<ICombatsHubClient>
{
    public CombatsHub(IHubConnections connections) : base(connections)
    {
    }
}
