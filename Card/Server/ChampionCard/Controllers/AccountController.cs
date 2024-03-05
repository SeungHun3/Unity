using ChampionCard.Action;
using Common.DTO;
using ChampionCard.Models.Game;
using ChampionCard.Models.Global;
using Microsoft.AspNetCore.Mvc;
using Server;
using StackExchange.Redis;

namespace ChampionCard.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController(GlobalContext globalContext, GameContext gameContext, IConnectionMultiplexer redis, DataTableManager table) : ControllerBase
    {
        private readonly GlobalContext _globalContext = globalContext;
        private readonly GameContext _gameContext = gameContext;
        private readonly IConnectionMultiplexer _redis = redis;
        private readonly DataTableManager _table = table;
        private readonly AccountAction _action = new(globalContext, gameContext, redis, table);

        [HttpPost("Login")]
        public async Task<ResLogin> Login(ReqLogin request)
        {
            return await _action.Login(request);
        }

        [HttpPost("DuplicateNickName")]
        public async Task<ResDuplicateNickName> DuplicateNickName(ReqDuplicateNickName request)
        {
            return await _action.DuplicateNickName(request);
        }
    }
}
