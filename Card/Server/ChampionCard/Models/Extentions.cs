using Common.Enum;
using Common.Game;

namespace ChampionCard.Models.Game;

public partial class UserDeck
{
    public UserDeckData GetUserDeck()
    {
        return new()
        {
            Spade1 = Spade1,
            Spade2 = Spade2,
            Spade3 = Spade3,
            Spade4 = Spade4,
            Spade5 = Spade5,
            Spade6 = Spade6,
            Spade7 = Spade7,
            Spade8 = Spade8,
            Spade9 = Spade9,
            Spade10 = Spade10,
            SpadeJ = SpadeJ,
            SpadeQ = SpadeQ,
            SpadeK = SpadeK,

            Dia1 = Dia1,
            Dia2 = Dia2,
            Dia3 = Dia3,
            Dia4 = Dia4,
            Dia5 = Dia5,
            Dia6 = Dia6,
            Dia7 = Dia7,
            Dia8 = Dia8,
            Dia9 = Dia9,
            Dia10 = Dia10,
            DiaJ = DiaJ,
            DiaQ = DiaQ,
            DiaK = DiaK,

            Heart1 = Heart1,
            Heart2 = Heart2,
            Heart3 = Heart3,
            Heart4 = Heart4,
            Heart5 = Heart5,
            Heart6 = Heart6,
            Heart7 = Heart7,
            Heart8 = Heart8,
            Heart9 = Heart9,
            Heart10 = Heart10,
            HeartJ = HeartJ,
            HeartQ = HeartQ,
            HeartK = HeartK,

            Clover1 = Clover1,
            Clover2 = Clover2,
            Clover3 = Clover3,
            Clover4 = Clover4,
            Clover5 = Clover5,
            Clover6 = Clover6,
            Clover7 = Clover7,
            Clover8 = Clover8,
            Clover9 = Clover9,
            CloverJ = CloverJ,
            CloverQ = CloverQ,
            CloverK = CloverK,

            Joker = Joker,

            Real1 = Real1,
            Real2 = Real2,
            Real3 = Real3,
            Real4 = Real4,
            Real5 = Real5,
            Real6 = Real6,
            Real7 = Real7,
            Real8 = Real8,
            Real9 = Real9
        };
    }
}

public partial class UserInventory
{
    public UserInventoryData GetUserInventory()
    {
        return new() { ItemType = (ItemType)ItemType, ItemSN = ItemSn, ItemCount = ItemCount };
    }
}