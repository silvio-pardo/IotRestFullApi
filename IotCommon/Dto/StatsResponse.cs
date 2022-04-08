using System;

namespace IotCommon.Dto
{
    public class StatsResponse : BaseResponse
    {
        public DateTime LastUpdate { get; set; }
        public string Payload { get; set; }
        public string DeviceID { get; set; }
    }
}
