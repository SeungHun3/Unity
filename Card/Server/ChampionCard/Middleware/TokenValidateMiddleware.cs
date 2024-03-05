using Microsoft.Extensions.Primitives;
using StackExchange.Redis;
using Common.Enum;
using Common.Game;

namespace ChampionCard.Middleware
{
    public class TokenValidateMiddleware(RequestDelegate next, IConnectionMultiplexer redis)
    {
        private readonly RequestDelegate _next = next;
        private readonly IConnectionMultiplexer _redis = redis;

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.Value is null)
                await _next(httpContext);

            if (httpContext.Request.Path.Value == "/Account/Login")
            {
                await _next(httpContext);
                return;
            }

            if (httpContext.Request.Headers.TryGetValue("AccountSN", out StringValues accountSN) is false)
                throw new CustomException() { ErrorCode = ErrorCode.InvalidRequest, CustomMessage = "Must Be Include Header (AccountSN)"};

            if (httpContext.Request.Headers.TryGetValue("Token", out StringValues token) is false)
                throw new CustomException() { ErrorCode = ErrorCode.InvalidRequest, CustomMessage = "Must Be Include Header (Token)" };

            var db = _redis.GetDatabase(RedisDB.Token);
            if (db is null)
            {
                Console.WriteLine($"{this.GetType().FullName}, Can't Connect RedisDB");
                throw new CustomException() { ErrorCode = ErrorCode.Maintenance, CustomMessage = "Can't Connect RedisDB" };
            }

            var storeToken = await db.StringGetAsync($"{accountSN}");

            if (token.ToString() != storeToken.ToString())
                throw new CustomException() { ErrorCode = ErrorCode.InvalidRequest, CustomMessage = "Invalid Game Token" };
           
            await _next(httpContext);
        }
    }
}
