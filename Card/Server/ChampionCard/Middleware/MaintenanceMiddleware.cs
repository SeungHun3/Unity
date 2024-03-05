using Common.Enum;
using Common.Game;
using StackExchange.Redis;

namespace ChampionCard.Middleware
{
    public class MaintenanceMiddleware(RequestDelegate next, IConnectionMultiplexer redis)
    {
        private readonly RequestDelegate _next = next;
        private readonly IConnectionMultiplexer _redis = redis;

        public async Task Invoke(HttpContext httpContext)
        {
            var maintenance = await CheckMaintenance(_redis);
            string ip = string.Empty;
            var remote = httpContext.Request.HttpContext.Connection.RemoteIpAddress;
            if (remote is not null)
                ip = remote.ToString();

            if (ip == "121.134.21.189") 
            {
                Console.WriteLine($"{this.GetType().FullName}, Dev IP Connection");
            }
            else if (maintenance is true)
                throw new CustomException() { ErrorCode = ErrorCode.Maintenance, CustomMessage = "maintenance" };

            await _next(httpContext);
        }

        /// <summary>
        /// 레디스에 점검 유무 확인
        /// </summary>
        /// <param name="redis"></param>
        /// <returns>
        /// true : 점검중
        /// false : 점검아님
        /// </returns>
        public async Task<bool> CheckMaintenance(IConnectionMultiplexer redis)
        {
            var db = redis.GetDatabase(RedisDB.Maintenance);
            if (db is null)
            {
                Console.WriteLine($"{this.GetType().FullName}, Can't Connect RedisDB");
                return true;
            }
       
            var maintenanceStr = (await db.StringGetAsync("Maintenance")).ToString().ToUpper();
            if(string.IsNullOrEmpty(maintenanceStr) ||
                maintenanceStr.Equals("ON", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine($"{this.GetType().FullName}, Maintenance On, str:{maintenanceStr}");
                return true;
            }

            return false;
        }
    }
}
