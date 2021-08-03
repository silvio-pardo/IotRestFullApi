using IotRestFullApi.Dal;
using IotRestFullApi.Entities;
using System.Collections.Generic;
using System.Linq;

namespace IotRestFullApi.Repository
{
    public class DeviceRepository
    {
        private readonly IotContext iotContext;
        public DeviceRepository(IotContext iotContext)
        {
            this.iotContext = iotContext;
        }
        public Device Get(string key)
        {
            if (iotContext == null)
                return null;
            Device foundValue = iotContext.Device.Where(_ => _.Uid == key).FirstOrDefault();
            return foundValue;
        }
        public IList<Device> GetAll()
        {
            if (iotContext == null)
                return null;
            IList<Device> foundValue = iotContext.Device.Select(_ => new Device() { Uid = _.Uid, Name = _.Name, Type = _.Type}).ToList();
            return foundValue;
        }
        public bool Insert(Device data)
        {
            return true;
        }
    }
}
