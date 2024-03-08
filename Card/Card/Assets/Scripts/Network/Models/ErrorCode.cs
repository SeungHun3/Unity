namespace SlotMachineWebServer.Common.Enum
{
    public enum ErrorCode : uint
    {
        Success = 0,

        InvalidRequest,
        PartnerLoginFail,
        
        Maintenance,
        LackCredit,
        MissingDataTable,
        TokenExpired,
    }
}