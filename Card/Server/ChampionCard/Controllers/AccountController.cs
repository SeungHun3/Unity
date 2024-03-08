using ChampionCard.Action;
using Common.DTO;
using ChampionCard.Models.Game;
using ChampionCard.Models.Global;
using Microsoft.AspNetCore.Mvc;
using Server;
using StackExchange.Redis;
using Common.Game;
using Common.Enum;

namespace ChampionCard.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //public class AccountController(GlobalContext globalContext, GameContext gameContext, IConnectionMultiplexer redis, DataTableManager table) : ControllerBase
    public class AccountController(DataTableManager table) : ControllerBase
    {
        //private readonly GlobalContext _globalContext = globalContext;
        //private readonly GameContext _gameContext = gameContext;
        //private readonly IConnectionMultiplexer _redis = redis;
        private readonly DataTableManager _table = table;
        //private readonly AccountAction _action = new(globalContext, gameContext, redis, table);

        //[HttpPost("Login")]
        //public async Task<ResLogin> Login(ReqLogin request)
        //{
        //    return await _action.Login(request);
        //}

        [HttpPost("Login")]
        public ResLogin Login(ReqLogin request)
        {
            List<UserInventoryData> inventory = [];
            inventory.Add(new UserInventoryData() { ItemType = ItemType.Coin, ItemCount = 1, ItemSN = 1   });

            UserData user = new()
            {
                AccountSN = 1,
                AdvertiseAgree = true,
                CreatedAt = DateTime.Now,
                CurEnergy = 30,
                Exp = 99,
                Inventory = inventory,
                LastEnergyUpdateTime = DateTime.Now.Subtract(TimeSpan.FromHours(1)),
                TermsAgree = false,
                PrivacyAgree = false,
                Deck = new() { Clover1 = 1, Clover2 = 1 },
                LastLoginAt = DateTime.Now,
                Level = 255,
                MaxEnergy = 999,
                NickName = "테스트유저",
                NightPushAgree = false
            };

            return new() { User = user };
        }

        //[HttpPost("DuplicateNickName")]
        //public async Task<ResDuplicateNickName> DuplicateNickName(ReqDuplicateNickName request)
        //{
        //    return await _action.DuplicateNickName(request);
        //}
        [HttpPost("DuplicateNickName")]
        public ResDuplicateNickName DuplicateNickName(ReqDuplicateNickName request)
        {
            return new() { Result = true };
        }
    }
}
