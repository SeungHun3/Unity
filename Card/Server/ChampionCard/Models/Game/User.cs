using System;
using System.Collections.Generic;

namespace ChampionCard.Models.Game;

public partial class User
{
    public long AccountSn { get; set; }

    public short MaxEnergy { get; set; }

    public short CurEnergy { get; set; }

    public DateTime LastEnergyUpdateTime { get; set; }

    public int Level { get; set; }

    public int Exp { get; set; }
}
