using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IotCommon.Entities.Enum;

namespace IotRestFullApi.Entities
{
    public class Device
    {
        #region Properties
        [Key]
        public string Uid { get; set; }
        public string Name { get; set; }
        public DeviceType Type { get; set; }
        public virtual ICollection<Action> Actions { get; set; }
        public virtual ICollection<Command> Commands { get; set; }
        public virtual ICollection<Stats> Stats { get; set; }
        #endregion
    }
}
