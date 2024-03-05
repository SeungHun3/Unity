using Common.DTO;
using Microsoft.AspNetCore.Mvc;
using Server;
using StackExchange.Redis;

namespace ChampionCard.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PassController(IConnectionMultiplexer redis, DataTableManager table) : ControllerBase
    {
        private readonly IConnectionMultiplexer _redis = redis;
        private readonly DataTableManager _table = table;

        [HttpPost("PassInfo")]
        public async Task<ResPassInfo> PassInfo(ReqPassInfo request)
        {
            var passInfo = new ResPassInfo();
            return passInfo;
        }
    }
}
