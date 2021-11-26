using IotRestFullApi.Entities.Enum;
using System.Collections.Generic;

namespace IotRestFullApi.Dto
{
    public class DeviceResponse
    {
        public string Uid { get; set; }
        public string Name { get; set; }
        public DeviceType Type { get; set; }
        public ICollection<CommandResponse> Commands { get; set; }
        public ICollection<ActionResponse> Actions { get; set; }
        public ICollection<StatsResponse> Stats { get; set; }
    }
}
