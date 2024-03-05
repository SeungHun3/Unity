using System;
using System.Collections.Generic;

namespace ChampionCard.Models.Game;

public partial class UserInventory
{
    public long AccountSn { get; set; }

    public sbyte ItemType { get; set; }

    public int ItemSn { get; set; }

    public int ItemCount { get; set; }
}
