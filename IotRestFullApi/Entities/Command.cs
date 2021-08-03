using IotRestFullApi.Entities.Enum;
using System;

namespace IotRestFullApi.Entities
{
    public class Command : BaseEntities
    {
        public string Uid { get; set; }
        public DateTime Time { get; set; }
        public string Payload { get; set; }
        public CommandType Type { get; set; }
        public Device Device { get; set; }
    }
}
