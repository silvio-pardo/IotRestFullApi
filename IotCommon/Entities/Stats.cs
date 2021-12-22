using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IotCommon.Entities
{
    public class Stats : BaseEntities
    {
        #region Properties
        public DateTime LastUpdate { get; set; }
        public string Payload { get; set; }
        [ForeignKey(nameof(Device))]
        public string DeviceId { get; set; }
        #endregion

        #region Navigation Properties
        public virtual Device Device { get; set; }
        #endregion
    }
}
