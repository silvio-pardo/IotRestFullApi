using System;
using System.ComponentModel.DataAnnotations.Schema;
using IotCommon.Entities.Enum;

namespace IotRestFullApi.Entities
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
