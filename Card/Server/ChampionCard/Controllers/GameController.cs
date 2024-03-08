//using ChampionCard.Action;
//using Common.DTO;
//using ChampionCard.Models.Game;
//using ChampionCard.Models.Global;
//using Microsoft.AspNetCore.Mvc;
//using Server;
//using StackExchange.Redis;

//namespace ChampionCard.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class GameController(GlobalContext globalContext, GameContext gameContext, IConnectionMultiplexer redis, DataTableManager table) : ControllerBase
//    {
//        private readonly GlobalContext _globalContext = globalContext;
//        private readonly GameContext _gameContext = gameContext;
//        private readonly IConnectionMultiplexer _redis = redis;
//        private readonly DataTableManager _table = table;
//        private readonly GameAction _action = new GameAction(globalContext, gameContext, redis, table);
//    }
//}
