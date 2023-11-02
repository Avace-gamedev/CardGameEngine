using Newtonsoft.Json;

namespace PockedeckBattler.Server.Stores;

public class JsonFileStore<TData> : FileStore<TData>
{
    readonly ILogger<JsonFileStore<TData>> _logger;
    readonly JsonSerializerSettings _settings;

    public JsonFileStore(string directory, ILogger<JsonFileStore<TData>> logger, JsonSerializerSettings? settings = null, string extension = ".res") : base(
        directory,
        extension
    )
    {
        _logger = logger;
        _settings = settings ?? new JsonSerializerSettings();
    }

    protected override Task<TData?> Read(string filePath, Stream stream, CancellationToken cancellationToken)
    {
        using StreamReader streamReader = new(stream);
        using JsonReader jsonReader = new JsonTextReader(streamReader);

        JsonSerializer serializer = JsonSerializer.CreateDefault(_settings);
        TData? value = serializer.Deserialize<TData>(jsonReader);

        if (value != null && !Validate(value))
        {
            _logger.LogWarning("Validation failed on value loaded from file {filePath}", filePath);
            return Task.FromResult((TData?)default);
        }

        return Task.FromResult(value);
    }

    protected override Task Write(string filePath, Stream outputStream, TData value, CancellationToken cancellationToken)
    {
        JsonSerializer serializer = JsonSerializer.CreateDefault(_settings);

        using StreamWriter streamWriter = new(outputStream);
        using JsonTextWriter jsonWriter = new(streamWriter);

        serializer.Serialize(jsonWriter, value);

        return Task.CompletedTask;
    }

    protected virtual bool Validate(TData value)
    {
        return true;
    }
}
