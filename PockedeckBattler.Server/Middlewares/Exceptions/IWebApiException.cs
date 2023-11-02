using System.Net;

namespace PockedeckBattler.Server.Middlewares.Exceptions;

public interface IWebApiException
{
    HttpStatusCode Status { get; }
    string? Title { get; }
    string? Message { get; }
}
