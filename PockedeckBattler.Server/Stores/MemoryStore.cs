using System.Collections.Concurrent;

namespace PockedeckBattler.Server.Stores;

public class MemoryStore<TData> : IStore<TData>
{
    readonly ConcurrentDictionary<string, TData> _store = new();

    public Task<bool> Exists(string key, CancellationToken cancellationToken)
    {
        return Task.FromResult(_store.ContainsKey(key));
    }

    public async IAsyncEnumerable<TData> LoadAll(CancellationToken cancellationToken)
    {
        foreach (TData value in _store.Values)
        {
            yield return value;
        }
    }

    public Task<TData?> Load(string key, CancellationToken cancellationToken)
    {
        return Task.FromResult(_store.GetValueOrDefault(key));
    }

    public Task Save(string key, TData data, CancellationToken cancellationToken)
    {
        _store[key] = data;
        return Task.CompletedTask;
    }

    public Task Delete(string key, CancellationToken cancellationToken)
    {
        _store.Remove(key, out _);
        return Task.CompletedTask;
    }
}
