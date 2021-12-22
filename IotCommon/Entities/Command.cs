using IotCommon.Entities.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IotCommon.Entities
{
    public class Command : BaseEntities
    {
        #region Properties
        public string Uid { get; set; }
        public DateTime Time { get; set; }
        public string Payload { get; set; }
        public CommandStatus Status { get; set; }
        [ForeignKey(nameof(Device))]
        public string DeviceId { get; set; }
        #endregion

        #region Navigation Properties
        public virtual Device Device { get; set; }
        #endregion
    }
}
