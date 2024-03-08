using SlotMachineWebServer.Common.Enum;

namespace SlotMachineWebServer.Common.DTO
{
    public class ReqLogin
    {
        public string PartnerName { get; set; } = string.Empty;

        public string Uid { get; set; } = string.Empty;
    }

    public class ResLogin
    {
        public ErrorCode ErrorCode { get; set; } = ErrorCode.Success;
        /// <summary>
        /// 현재 크레딧
        /// </summary>
        public float Credit { get; set; }
        /// <summary>
        /// 파트너에서 제공된 토큰
        /// 요청 패킷 클래스 내에 첨부 요청
        /// </summary>
        public string Token_Partner { get; set; } = string.Empty;
        /// <summary>
        /// 자체 발급된 토큰
        /// 요청 패킷 헤더 내에 첨부 요청
        /// </summary>
        public string Token_Game { get; set; } = string.Empty;
    }
}
