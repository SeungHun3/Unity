using Common.DTO;
using ChampionCard.Models.Game;
using ChampionCard.Models.Global;
using Server;
using StackExchange.Redis;

namespace ChampionCard.Action
{
    public class GameAction(GlobalContext globalContext, GameContext gameContext, IConnectionMultiplexer redis, DataTableManager table)
    {
        private readonly GlobalContext _accountContext = globalContext;
        private readonly GameContext _gameContext = gameContext;
        private readonly IConnectionMultiplexer _redis = redis;
        private readonly DataTableManager _table = table;

    }
}
