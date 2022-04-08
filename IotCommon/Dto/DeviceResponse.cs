using IotCommon.Entities.Enum;

namespace IotCommon.Dto
{
    public class DeviceResponse
    {
        public string Uid { get; set; }
        public string Name { get; set; }
        public DeviceType Type { get; set; }
    }
}
