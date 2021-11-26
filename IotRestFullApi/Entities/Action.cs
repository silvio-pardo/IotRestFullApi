
using System.ComponentModel.DataAnnotations.Schema;

namespace IotRestFullApi.Entities
{
    public class Action : BaseEntities
    {
        #region Properties
        public string Uid { get; set; }
        public string Payload { get; set; }
        [ForeignKey(nameof(Device))]
        public string DeviceId { get; set; }
        #endregion

        #region Navigation Properties
        public virtual Device Device { get; set; }
        #endregion
    }
}
