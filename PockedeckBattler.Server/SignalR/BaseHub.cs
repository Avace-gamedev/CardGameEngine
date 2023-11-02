using Microsoft.AspNetCore.SignalR;

namespace PockedeckBattler.Server.SignalR;

public abstract class BaseHub<T> : Hub<T> where T: class, IHubClient
{
    readonly IHubConnections _connections;

    protected BaseHub(IHubConnections connections)
    {
        _connections = connections;
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _connections.UnregisterConnection(Context.ConnectionId);

        return base.OnDisconnectedAsync(exception);
    }

    public void DeclareIdentity(string playerName)
    {
        _connections.Register(playerName, Context.ConnectionId);
    }
}
