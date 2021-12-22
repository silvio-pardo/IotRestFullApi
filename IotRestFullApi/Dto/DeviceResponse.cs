using IotCommon.Entities.Enum;
using System.Collections.Generic;

namespace IotRestFullApi.Dto
{
    public class DeviceResponse
    {
        public string Uid { get; set; }
        public string Name { get; set; }
        public DeviceType Type { get; set; }
    }
}
