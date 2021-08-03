using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IotRestFullApi.Entities
{
    public class Stats : BaseEntities
    {
        public Device Device { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Payload { get; set; }
    }
}
