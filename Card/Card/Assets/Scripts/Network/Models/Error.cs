using SlotMachineWebServer.Common.Enum;

namespace SlotMachineWebServer.Common.DTO
{
    public class ResError
    {
        public ErrorCode ErrorCode { get; set; }
        public string? ErrorDetail { get; set; } 
    }
}
