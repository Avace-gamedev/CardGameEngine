namespace PockedeckBattler.Server.SignalR;

public interface IHubClient
{
    Task<string> GetIdentity();
}
