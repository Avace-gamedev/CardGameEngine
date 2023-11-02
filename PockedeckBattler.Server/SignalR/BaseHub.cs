using Microsoft.AspNetCore.SignalR;

namespace PockedeckBattler.Server.SignalR;

public abstract class BaseHub<T> : Hub<T> where T: class, IHubClient
{
    readonly IHubConnections _connections;

    protected BaseHub(IHubConnections connections)
    {
        _connections = connections;

    }

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }
}
