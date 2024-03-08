using Mongs.API;
using System;
using System.Collections.Generic;

namespace SlotServer.Models
{
    [Serializable]
    public class SlotResultModelRequest : RequestBase
    {
        public string user_uid { get; set; }
        public string site_uid { get; set; }
        public long slotmachine_no { get; set; }
        public int bet_step { get; set; }
        public int money_step { get; set; }
    }
    
    [Serializable]
    public class SlotResultModelResponse : ResponseBase
    {
        [Serializable]
        public class PayLineData
        {
            public long serial_no { get; set; }
            public int index { get; set; }
        }

        [Serializable]

        public class ResultData
        {
            public List<PayLineData> paylinedatalist { get; set; }
            public List<long> symbollist { get; set; }
            public float resultpoint { get; set; }
            public int freespin_count { get; set; }
        }

        public ResultData[] result_array { get; set; } 
        public long point { get; set; }
    }
}
