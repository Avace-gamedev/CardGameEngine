using System.Runtime.CompilerServices;

namespace PockedeckBattler.Server.Stores;

public abstract class FileStore<TData> : IStore<TData>
{
    readonly string _directory;
    readonly string _extension;

    protected FileStore(string directory, string extension = ".res")
    {
        _directory = directory;
        _extension = extension.StartsWith('.') ? extension : $".{extension}";
    }

    public Task<bool> Exists(string key, CancellationToken cancellationToken)
    {
        string filePath = GetFilePath(key, out _);
        return Task.FromResult(File.Exists(filePath));
    }

    public async IAsyncEnumerable<TData> LoadAll([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        if (!Directory.Exists(_directory))
        {
            yield break;
        }

        foreach (string file in Directory.EnumerateFiles(_directory, $"*{_extension}"))
        {
            TData? data = await LoadFromFile(file, cancellationToken);
            if (data != null)
            {
                yield return data;
            }
        }
    }

    public async Task<TData?> Load(string key, CancellationToken cancellationToken = default)
    {
        string filePath = GetFilePath(key, out _);
        if (!File.Exists(filePath))
        {
            return default;
        }

        return await LoadFromFile(filePath, cancellationToken);
    }

    public async Task Save(string key, TData value, CancellationToken cancellationToken = default)
    {
        if (!Directory.Exists(_directory))
        {
            Directory.CreateDirectory(_directory);
        }

        string filePath = GetFilePath(key, out string fileName);
        string backupFilePath = GetBackupFilePath(fileName, out _);

        if (File.Exists(filePath))
        {
            File.Move(filePath, backupFilePath, true);
        }

        try
        {
            await using FileStream stream = File.OpenWrite(filePath);
            await Write(filePath, stream, value, cancellationToken);
        }
        catch
        {
            File.Move(backupFilePath, filePath);
        }
        finally
        {
            File.Delete(backupFilePath);
        }
    }

    public Task Delete(string key, CancellationToken cancellationToken)
    {
        string filePath = GetFilePath(key, out _);
        File.Delete(filePath);

        return Task.CompletedTask;
    }

    protected abstract Task<TData?> Read(string filePath, Stream stream, CancellationToken cancellationToken);
    protected abstract Task Write(string filePath, Stream outputStream, TData value, CancellationToken cancellationToken);

    async Task<TData?> LoadFromFile(string filePath, CancellationToken cancellationToken)
    {
        await using FileStream stream = File.OpenRead(filePath);
        return await Read(filePath, stream, cancellationToken);
    }

    string GetFilePath(string key, out string fileName)
    {
        fileName = key + _extension;
        string filePath = Path.Join(_directory, fileName);
        return filePath;
    }

    string GetBackupFilePath(string fileName, out string backupFileName)
    {
        backupFileName = fileName + ".bak";
        string backupFilePath = Path.Join(_directory, backupFileName);
        return backupFilePath;
    }
}
