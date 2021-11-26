using IotRestFullApi.Dal;
using IotRestFullApi.Dto;
using IotRestFullApi.Entities;
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
            .Include(_ => _.Commands)
            .Include(_ => _.Actions)
            .Include(_ => _.Stats)
            .AsSingleQuery()
            .Select(_ => new DeviceResponse()
            {
                Uid = _.Uid,
                Name = _.Name,
                Type = _.Type,
                Commands = _.Commands
                .Select(x => new CommandResponse()
                {
                    Id = x.Id,
                    Payload = x.Payload,
                    Uid = x.Uid,
                    Time = x.Time,
                    Status = x.Status,
                    DeviceID = x.Device.Uid
                })
                .ToList(),
                Actions = _.Actions
                .Select(y => new ActionResponse()
                {
                    Id = y.Id,
                    Payload = y.Payload,
                    Uid = y.Uid,
                    DeviceID = y.Device.Uid
                })
                .ToList(),
                Stats = _.Stats.Select(z => new StatsResponse()
                {
                    Id = z.Id,
                    Payload = z.Payload,
                    LastUpdate = z.LastUpdate,
                    DeviceID = z.Device.Uid
                })
                .ToList()
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
                .Include(_ => _.Commands)
                .Include(_ => _.Actions)
                .Include(_ => _.Stats)
                .AsSingleQuery()
                .Select(_ => new DeviceResponse()
                {
                    Uid = _.Uid,
                    Name = _.Name,
                    Type = _.Type,
                    Commands = _.Commands
                    .Select(x => new CommandResponse() 
                    { 
                        Id = x.Id, 
                        Payload = x.Payload, 
                        Uid = x.Uid, 
                        Time = x.Time, 
                        Status = x.Status,
                        DeviceID = x.Device.Uid
                    })
                    .ToList(),
                    Actions = _.Actions
                    .Select(y => new ActionResponse()
                    { 
                        Id = y.Id,
                        Payload = y.Payload,
                        Uid = y.Uid,
                        DeviceID = y.Device.Uid
                    })
                    .ToList(),
                    Stats = _.Stats.Select(z => new StatsResponse()
                    {
                        Id = z.Id,
                        Payload = z.Payload,
                        LastUpdate = z.LastUpdate,
                        DeviceID = z.Device.Uid
                    })
                    .ToList()
                })
                .ToList();
            return foundValue;
        }
    }
}
