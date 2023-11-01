namespace PockedeckBattler.Server.Stores;

public class InvalidCombatConfigurationException : Exception
{
    public InvalidCombatConfigurationException(string? message) : base(message)
    {
    }
}
