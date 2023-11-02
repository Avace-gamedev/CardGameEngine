﻿namespace PockedeckBattler.Server.SignalR;

public interface IHubConnections
{
    void Register(string playerName, string connectionId);
    string? GetPlayer(string connectionId);
    string? GetConnection(string playerName);
}
