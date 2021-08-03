using IotRestFullApi.Entities.Enum;
using System;

namespace IotRestFullApi.Entities
{
    public class Action : BaseEntities
    {
        public string Uid { get; set; }
        public DateTime Time { get; set; }
        public string Payload { get; set; }
        public ActionType Type { get; set; }
    }
}
