using Common.Enum;

namespace Common.DTO
{
    public class ResError
    {
        public ErrorCode ErrorCode { get; set; }
        public string? ErrorDetail { get; set; }
    }
}
