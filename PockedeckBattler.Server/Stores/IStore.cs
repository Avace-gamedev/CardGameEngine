namespace PockedeckBattler.Server.Stores;

public interface IStore<TData>
{
    Task<bool> Exists(string key, CancellationToken cancellationToken);
    IAsyncEnumerable<TData> LoadAll(CancellationToken cancellationToken);
    Task<TData?> Load(string key, CancellationToken cancellationToken);
    Task Save(string key, TData data, CancellationToken cancellationToken);
    Task Delete(string key, CancellationToken cancellationToken);
}
