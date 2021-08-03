using IotRestFullApi.Entities.Enum;
using System.ComponentModel.DataAnnotations;

namespace IotRestFullApi.Entities
{
    public class Device
    {
        [Key]
        public string Uid { get; set; }
        public string Name { get; set; }
        public DeviceType Type { get; set; }
    }
}
