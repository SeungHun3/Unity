using Mongs.API;
using System;
using System.Collections.Generic;

namespace SlotServer.Models
{
    public class LoginModelRequest : RequestBase
    {
        public string user_uid { get; set; }
        public string site_uid { get; set; }
        public long slotmachine_no { get; set; }
    }

    public class LoginModelResponse : ResponseBase
    {
        public string nickname { get; set; }
        public long point { get; set; }
    }

    public class ReqLogin : RequestBase
    {
        public Common.Enum.PlatformType PlatformType { get; set; }
        public string PlatformID { get; set; } = string.Empty;
    }

    public class ResLogin : ResponseBase
    {
        public Common.Game.UserData User { get; set; } = new();
    }
}

// 서버, 클라 공용
namespace Common.Enum
{
    public enum PlatformType
    {
        Guest,
    }
}
namespace Common.Game
{
    public class UserData
    {
        #region Account
        public long AccountSN { get; set; }
        public string NickName { get; set; } = string.Empty;
        public bool TermsAgree { get; set; }
        public bool PrivacyAgree { get; set; }
        public bool AdvertiseAgree { get; set; }
        public bool NightPushAgree { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastLoginAt { get; set; }
        #endregion

        #region User
        public short MaxEnergy { get; set; }
        public short CurEnergy { get; set; }
        public DateTime LastEnergyUpdateTime { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }
        #endregion

        #region UserInventory
        public List<UserInventoryData> Inventory { get; set; } = new();
        #endregion

        #region UserDeck
        public UserDeckData Deck { get; set; } = new();
        #endregion
    }
}


namespace Common.Game
{
    public class UserInventoryData
    {
        public ItemType ItemType { get; set; }
        public int ItemSN { get; set; }
        public int ItemCount { get; set; }
    }
}
public enum ItemType
{
    Coin,
    Gold,

}
namespace Common.Game
{
    public class UserDeckData
    {
        public int Spade1 { get; set; }

        public int Spade2 { get; set; }

        public int Spade3 { get; set; }

        public int Spade4 { get; set; }

        public int Spade5 { get; set; }

        public int Spade6 { get; set; }

        public int Spade7 { get; set; }

        public int Spade8 { get; set; }

        public int Spade9 { get; set; }

        public int Spade10 { get; set; }

        public int SpadeJ { get; set; }

        public int SpadeQ { get; set; }

        public int SpadeK { get; set; }

        public int Dia1 { get; set; }

        public int Dia2 { get; set; }

        public int Dia3 { get; set; }

        public int Dia4 { get; set; }

        public int Dia5 { get; set; }

        public int Dia6 { get; set; }

        public int Dia7 { get; set; }

        public int Dia8 { get; set; }

        public int Dia9 { get; set; }

        public int Dia10 { get; set; }

        public int DiaJ { get; set; }

        public int DiaQ { get; set; }

        public int DiaK { get; set; }

        public int Heart1 { get; set; }

        public int Heart2 { get; set; }

        public int Heart3 { get; set; }

        public int Heart4 { get; set; }

        public int Heart5 { get; set; }

        public int Heart6 { get; set; }

        public int Heart7 { get; set; }

        public int Heart8 { get; set; }

        public int Heart9 { get; set; }

        public int Heart10 { get; set; }

        public int HeartJ { get; set; }

        public int HeartQ { get; set; }

        public int HeartK { get; set; }

        public int Clover1 { get; set; }

        public int Clover2 { get; set; }

        public int Clover3 { get; set; }

        public int Clover4 { get; set; }

        public int Clover5 { get; set; }

        public int Clover6 { get; set; }

        public int Clover7 { get; set; }

        public int Clover8 { get; set; }

        public int Clover9 { get; set; }

        public int Clover10 { get; set; }

        public int CloverJ { get; set; }

        public int CloverQ { get; set; }

        public int CloverK { get; set; }

        public int Joker { get; set; }

        public int Real1 { get; set; }

        public int Real2 { get; set; }

        public int Real3 { get; set; }

        public int Real4 { get; set; }

        public int Real5 { get; set; }

        public int Real6 { get; set; }

        public int Real7 { get; set; }

        public int Real8 { get; set; }

        public int Real9 { get; set; }
    }
}
