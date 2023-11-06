namespace CardGame.Engine.Combats.Persistence;

public interface ICombatInstanceSerializer
{
    public Task SerializeAsync(Stream stream, CombatInstance instance);
    public Task<CombatInstance?> DeserializeAsync(Stream stream);
}
