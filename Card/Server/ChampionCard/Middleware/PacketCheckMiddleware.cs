using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using StackExchange.Redis;
using Common.Game;
using Common.Enum;

namespace ChampionCard.Middleware
{
    public class PacketCheckMiddleware(RequestDelegate next, IConnectionMultiplexer redis)
    {
        private readonly RequestDelegate _next = next;
        private readonly IConnectionMultiplexer _redis = redis;
        private readonly string[] _excludeList = ["/", "/Error", "/favicon.ico"];
        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.Value is null || _excludeList.Contains(httpContext.Request.Path.Value))
            {
                await _next(httpContext);
                return;
            }

            if (httpContext.Request.Headers.TryGetValue("AccountSN", out StringValues accountSN) is false)
                throw new CustomException() { ErrorCode = ErrorCode.InvalidRequest, CustomMessage = "Must Be Include Header (AccountSN)" };

            if (httpContext.Request.Headers.TryGetValue("Seq", out StringValues curSeqStr) is false)
                throw new CustomException() { ErrorCode = ErrorCode.InvalidRequest, CustomMessage = "Must Be Include Header (Seq)" };

            if(string.IsNullOrEmpty(accountSN) || string.IsNullOrEmpty(curSeqStr))
            {
                await _next(httpContext);
                return;
            }

            var db = _redis.GetDatabase((int)RedisDB.Packet);
            if (db is null)
            {
                Console.WriteLine($"{this.GetType().FullName}, Can't Connect RedisDB");
                throw new CustomException() { ErrorCode = ErrorCode.InvalidRequest, CustomMessage = "Can't Connect RedisDB" };
            }

            var lastSeq = (int)await db.StringGetAsync(Convert.ToString(accountSN));
            var curSeq = int.Parse(curSeqStr.ToString());

            if (lastSeq > 1 && curSeq < lastSeq)
            {
                var lastPacket = db.StringGetAsync(Convert.ToString(accountSN) + "_Packet").ToString();
                if (string.IsNullOrEmpty(lastPacket) is false)
                {
                    // 마지막 패킷 레디스에 값이 있으면 보낸다.
                    var okResult = new OkObjectResult(lastPacket);
                    await httpContext.Response.WriteAsJsonAsync(okResult);
                    return;
                }
                else
                {
                    throw new CustomException() { ErrorCode = ErrorCode.InvalidRequest, CustomMessage = "Packet Req Error, Required Re Login" };
                }
            }

            await _next(httpContext);
        }
    }
}
