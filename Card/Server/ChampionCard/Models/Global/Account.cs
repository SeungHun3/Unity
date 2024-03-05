using System;
using System.Collections.Generic;

namespace ChampionCard.Models.Global;

/// <summary>
/// 	
/// </summary>
public partial class Account
{
    public long AccountSn { get; set; }

    public string NickName { get; set; } = null!;

    public bool TermsAgree { get; set; }

    public bool PrivacyAgree { get; set; }

    public bool AdvertiseAgree { get; set; }

    public bool NightPushAgree { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastLoginAt { get; set; }
}
