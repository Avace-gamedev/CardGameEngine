using Newtonsoft.Json;

namespace PockedeckBattler.Server.Stores;

public class JsonFileStore<T> : FileStore<T>
{
    readonly JsonSerializerSettings _settings;

    public JsonFileStore(string directory, JsonSerializerSettings? settings = null, string extension = ".res") : base(directory, extension)
    {
        _settings = settings ?? new JsonSerializerSettings();
    }

    protected override Task<T?> Read(Stream stream, CancellationToken cancellationToken)
    {
        using StreamReader streamReader = new(stream);
        using JsonReader jsonReader = new JsonTextReader(streamReader);

        JsonSerializer serializer = JsonSerializer.CreateDefault(_settings);
        T? value = serializer.Deserialize<T>(jsonReader);

        return Task.FromResult(value);
    }

    protected override Task Write(T value, Stream outputStream, CancellationToken cancellationToken)
    {
        JsonSerializer serializer = JsonSerializer.CreateDefault(_settings);

        using StreamWriter streamWriter = new(outputStream);
        using JsonTextWriter jsonWriter = new(streamWriter);

        serializer.Serialize(jsonWriter, value);

        return Task.CompletedTask;
    }
}
