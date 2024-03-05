namespace Common.DTO
{
    public class ReqLogin
    {
        public Common.Enum.PlatformType PlatformType { get; set; }
        public string PlatformID { get; set; } = string.Empty;
    }

    public class ResLogin
    {
        public Common.Game.UserData User { get; set; } = new();
    }

    public class ReqDuplicateNickName
    {
        public string NickName { get; set; } = string.Empty;
    }

    public class ResDuplicateNickName
    {
        public bool Result { get; set; }
    }
}
