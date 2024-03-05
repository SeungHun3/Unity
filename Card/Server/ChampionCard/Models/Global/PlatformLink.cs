using System;
using System.Collections.Generic;

namespace ChampionCard.Models.Global;

public partial class PlatformLink
{
    public long Sn { get; set; }

    /// <summary>
    /// 플랫폼별 키
    /// </summary>
    public int PlatformType { get; set; }

    public string PlatformId { get; set; } = null!;

    /// <summary>
    /// Account SN 
    /// </summary>
    public long AccountSn { get; set; }
}
