using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IotRestFullApi.Entities
{
    public class Stats : BaseEntities
    {
        public DateTime LastUpdate { get; set; }
        public string Payload { get; set; }
        public Device Device { get; set; }
    }
}
