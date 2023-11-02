using CardGame.Engine.Combats.Exceptions;
using Microsoft.AspNetCore.Mvc;
using PockedeckBattler.Server.Middlewares.Exceptions;

namespace PockedeckBattler.Server.Rest.Exceptions;

public class GameEngineExceptionHandler : IWebApiExceptionHandler
{
    public ProblemDetails? Handle(Exception exn)
    {
        switch (exn)
        {
            case InvalidMoveException invalidMoveException:
                return new ProblemDetails
                {
                    Title = "Invalid move",
                    Detail = invalidMoveException.Message,
                    Status = StatusCodes.Status400BadRequest
                };
        }

        return null;
    }
}
