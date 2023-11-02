using Microsoft.AspNetCore.Mvc;

namespace PockedeckBattler.Server.Middlewares.Exceptions;

public interface IWebApiExceptionHandler
{
    ProblemDetails? Handle(Exception exn);
}
