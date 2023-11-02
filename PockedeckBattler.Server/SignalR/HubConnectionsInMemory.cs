using System.Collections.Concurrent;

namespace PockedeckBattler.Server.SignalR;

public class HubConnectionsInMemory : IHubConnections
{
    readonly ConcurrentDictionary<string, string> _connectionToPlayerName = new();
    readonly ILogger<HubConnectionsInMemory> _logger;
    readonly ConcurrentDictionary<string, string> _playerNameToConnection = new();

    public HubConnectionsInMemory(ILogger<HubConnectionsInMemory> logger)
    {
        _logger = logger;
    }

    public void Register(string playerName, string connectionId)
    {
        UnregisterPlayer(playerName);
        UnregisterConnection(connectionId);

        _playerNameToConnection[playerName] = connectionId;
        _connectionToPlayerName[connectionId] = playerName;

        _logger.LogInformation("Player {name} has been registered", playerName);
    }

    public string? GetPlayer(string connectionId)
    {
        return _connectionToPlayerName.GetValueOrDefault(connectionId);
    }

    public string? GetConnection(string playerName)
    {
        return _playerNameToConnection.GetValueOrDefault(playerName);
    }

    public void UnregisterPlayer(string playerName)
    {
        if (!_playerNameToConnection.TryGetValue(playerName, out string? oldConnectionId))
        {
            return;
        }

        _playerNameToConnection.Remove(playerName, out _);
        _connectionToPlayerName.Remove(oldConnectionId, out _);

        _logger.LogInformation("Player {name} has been unregistered", playerName);
    }

    public void UnregisterConnection(string connectionId)
    {
        if (!_connectionToPlayerName.TryGetValue(connectionId, out string? oldPlayerName))
        {
            return;
        }

        _playerNameToConnection.Remove(oldPlayerName, out _);
        _connectionToPlayerName.Remove(connectionId, out _);

        _logger.LogInformation("Player {name} has been unregistered", oldPlayerName);
    }
}
