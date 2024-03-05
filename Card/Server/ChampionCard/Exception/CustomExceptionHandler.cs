using Microsoft.AspNetCore.Diagnostics;
using Common.DTO;
using Common.Game;

namespace ChampionCard.ExceptionHandler
{
    public class CustomExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, System.Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Called CustomExceptionHandler");

            if (exception is CustomException ce)
            {
                httpContext.Response.StatusCode = 512;
                await httpContext.Response.WriteAsJsonAsync(new ResError() { ErrorCode = ce.ErrorCode, ErrorDetail = ce.CustomMessage }, cancellationToken: cancellationToken);
                return true;
            }

            return false;
        }
    }
}
