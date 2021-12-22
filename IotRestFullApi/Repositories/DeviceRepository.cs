using IotRestFullApi.Dal;
using IotRestFullApi.Dto;
using IotCommon.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IotRestFullApi.Repositories
{
    public class DeviceRepository : BaseRepositories<Device>
    {
        public DeviceRepository(IotContext iotContext) : base(iotContext)
        {
        }
        public DeviceResponse Get(string key)
        {
            if (iotContext == null)
                return null;
            DeviceResponse foundValue = iotContext.Device
            .Where(_ => _.Uid == key)
            .Select(_ => new DeviceResponse()
            {
                 Uid = _.Uid,
                 Name = _.Name,
                 Type = _.Type
            })
            .ToList()
            .FirstOrDefault();
            return foundValue;
        }
        public IList<DeviceResponse> GetAll()
        {
            if (iotContext == null)
                return null;
            IList<DeviceResponse> foundValue = iotContext.Device
                .Select(_ => new DeviceResponse()
                {
                    Uid = _.Uid,
                    Name = _.Name,
                    Type = _.Type
                })
                .ToList();
            return foundValue;
        }
    }
}
