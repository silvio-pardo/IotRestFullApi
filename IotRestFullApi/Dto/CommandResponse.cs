using IotRestFullApi.Entities.Enum;
using System;

namespace IotRestFullApi.Dto
{
    public class CommandResponse : BaseResponse
    {
        public string Uid { get; set; }
        public DateTime Time { get; set; }
        public string Payload { get; set; }
        public CommandStatus Status { get; set; }
        public string DeviceID { get; set; }
    }
}
