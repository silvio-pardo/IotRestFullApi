﻿using IotRestFullApi.Entities.Enum;

namespace IotRestFullApi.Entities
{
    public class Action : BaseEntities
    {
        public string Uid { get; set; }
        public ActionStatus Status { get; set; }
        public string Payload { get; set; }
        public Device Device { get; set; }
    }
}