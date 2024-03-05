using ChampionCard.Models.Game;
using ChampionCard.Models.Global;
using Common.DTO;
using Common.Enum;
using Common.Game;
using Microsoft.EntityFrameworkCore;
using Server;
using StackExchange.Redis;

namespace ChampionCard.Action
{
    public class AccountAction(GlobalContext globalContext, GameContext gameContext, IConnectionMultiplexer redis, DataTableManager table)
    {
        private readonly GlobalContext _accountContext = globalContext;
        private readonly GameContext _gameContext = gameContext;
        private readonly IConnectionMultiplexer _redis = redis;
        private readonly DataTableManager _table = table;

        public async Task<ResLogin> Login(ReqLogin req)
        {
            var platformLinkData = await _accountContext.PlatformLinks.AsNoTracking().FirstOrDefaultAsync(p => p.PlatformId == req.PlatformID && p.PlatformType == (int)req.PlatformType);
            if (platformLinkData is null)
                throw new CustomException() { ErrorCode = ErrorCode.CreateUser, CustomMessage = "CreateUser Call" };
            else
            {
                var accountData = await _accountContext.Accounts.FirstAsync(p => p.AccountSn == platformLinkData.AccountSn);
                var userData = await _gameContext.Users.FirstAsync(p=>p.AccountSn == platformLinkData.AccountSn);
                var inventoryData = await _gameContext.UserInventories.Where(p => p.AccountSn == platformLinkData.AccountSn).ToListAsync();
                var deckData = await _gameContext.UserDecks.FirstAsync(p => p.AccountSn == platformLinkData.AccountSn);

                UserData user = new UserData()
                {
                    AccountSN = platformLinkData.AccountSn,
                    NickName = accountData.NickName,
                    TermsAgree = accountData.TermsAgree,
                    PrivacyAgree = accountData.PrivacyAgree,
                    AdvertiseAgree = accountData.AdvertiseAgree,
                    NightPushAgree = accountData.NightPushAgree,
                    CreatedAt = accountData.CreatedAt,
                    LastLoginAt = accountData.LastLoginAt,

                    MaxEnergy = userData.MaxEnergy,
                    CurEnergy = userData.CurEnergy,
                    LastEnergyUpdateTime = userData.LastEnergyUpdateTime,
                    Level = userData.Level,
                    Exp = userData.Exp,

                    Inventory = inventoryData.Select(p => p.GetUserInventory()).ToList(),

                    Deck = deckData.GetUserDeck()
                };

                return new() { User = user };
            }
        }

        public async Task<ResDuplicateNickName> DuplicateNickName(ReqDuplicateNickName req)
        {
            var temp = await _accountContext.Accounts.AsNoTracking().FirstOrDefaultAsync(p => p.NickName == req.NickName);
            if(temp is not null)
            {
                return new ResDuplicateNickName() { Result = false };
            }
            else
            {
                return new ResDuplicateNickName() { Result = true };
            }
        }

    }
}
