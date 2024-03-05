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
        public List<UserInventoryData> Inventory { get; set; } = [];
        #endregion

        #region UserDeck
        public UserDeckData Deck { get; set; } = new();
        #endregion
    }
}
