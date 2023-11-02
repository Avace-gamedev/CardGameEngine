using System.Runtime.CompilerServices;

namespace PockedeckBattler.Server.Stores;

public abstract class SerializedDataStore<TData, TSerializedData> : IStore<TData>
{
    readonly ILogger<SerializedDataStore<TData, TSerializedData>> _logger;
    readonly IStore<TSerializedData> _serializedDataStore;

    protected SerializedDataStore(IStore<TSerializedData> serializedDataStore, ILogger<SerializedDataStore<TData, TSerializedData>> logger)
    {
        _serializedDataStore = serializedDataStore;
        _logger = logger;
    }

    public Task<bool> Exists(string key, CancellationToken cancellationToken)
    {
        return _serializedDataStore.Exists(key, cancellationToken);
    }

    public async IAsyncEnumerable<TData> LoadAll([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        IAsyncEnumerable<TSerializedData> serializedValues = _serializedDataStore.LoadAll(cancellationToken);
        await foreach (TSerializedData serializedValue in serializedValues)
        {
            TData? value = Deserialize(serializedValue);
            if (value == null)
            {
                _logger.LogWarning("Could not deserialize value {value}", serializedValue);
                continue;
            }

            yield return value;
        }
    }

    public async Task<TData?> Load(string key, CancellationToken cancellationToken)
    {
        TSerializedData? serializedValue = await _serializedDataStore.Load(key, cancellationToken);
        if (serializedValue == null)
        {
            return default;
        }

        return Deserialize(serializedValue);
    }

    public async Task Save(string key, TData data, CancellationToken cancellationToken)
    {
        TSerializedData serializedValue = Serialize(data);
        await _serializedDataStore.Save(key, serializedValue, cancellationToken);
    }

    public Task Delete(string key, CancellationToken cancellationToken)
    {
        return _serializedDataStore.Delete(key, cancellationToken);
    }

    protected abstract TSerializedData Serialize(TData value);
    protected abstract TData? Deserialize(TSerializedData serializedValue);
}
