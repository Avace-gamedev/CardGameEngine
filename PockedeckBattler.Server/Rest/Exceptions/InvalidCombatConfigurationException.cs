namespace PockedeckBattler.Server.Rest.Exceptions;

public class InvalidCombatConfigurationException : Exception
{
    public InvalidCombatConfigurationException(string? message) : base(message)
    {
    }
}
