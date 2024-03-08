using SlotMachineWebServer.Common.Enum;
using System;
using System.Collections.Generic;

namespace SlotMachineWebServer.Common.DTO
{
    public class ReqSlot
    {
        public string PartnerName { get; set; } = string.Empty;
        public string Uid { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public float Credit { get; set; }
    }

    public class ResSlot
    {
        public ErrorCode ErrorCode { get; set; }
        public List<GameResult>? GameResults { get; set; }
        public float ResultCredit { get; set; }
    }

    public class GameResult
    {
        public List<ulong>? Symbols { get; set; }
        public List<ulong>? MachedPattern { get; set; }
        public bool BigWin { get; set; }
        public bool Scatter { get; set; }
    }
}
