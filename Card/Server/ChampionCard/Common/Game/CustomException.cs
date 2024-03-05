using Common.Enum;

namespace Common.Game
{
    public class CustomException : Exception
    {
        public ErrorCode ErrorCode { get; set; }
        public string CustomMessage { get; set; } = string.Empty;
    }
}
