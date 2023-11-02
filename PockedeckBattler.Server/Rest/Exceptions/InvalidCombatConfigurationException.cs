using System.Net;
using PockedeckBattler.Server.Middlewares.Exceptions;

namespace PockedeckBattler.Server.Rest.Exceptions;

public class InvalidCombatConfigurationException : Exception, IWebApiException
{
    public InvalidCombatConfigurationException(string? message) : base(message)
    {
    }

    public string? Title { get; init; }
    public HttpStatusCode Status => HttpStatusCode.BadRequest;
}
