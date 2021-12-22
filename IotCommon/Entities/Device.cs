using IotCommon.Entities.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IotCommon.Entities
{
    public class Device
    {
        #region Properties
        [Key]
        public string Uid { get; set; }
        public string Name { get; set; }
        public DeviceType Type { get; set; }
        public ICollection<Action> Actions { get; set; }
        public ICollection<Command> Commands { get; set; }
        public ICollection<Stats> Stats { get; set; }
        #endregion
    }
}
