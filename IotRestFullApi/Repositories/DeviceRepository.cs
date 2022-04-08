using IotRestFullApi.Dal;
using IotCommon.Dto;
using System.Collections.Generic;
using System.Linq;
using IotRestFullApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace IotRestFullApi.Repositories
{
    public class DeviceRepository
    {
        protected readonly IotContext iotContext;
        protected DbSet<Device> DbSet { get { return iotContext.Set<Device>(); } }
        public DeviceRepository(IotContext iotContext)
        {
            this.iotContext = iotContext;
        }
        public Device Single(string id)
        {
            return DbSet.Find(id);
        }
        public Device Insert(Device data)
        {
            iotContext.Add(data);
            iotContext.SaveChanges();
            return data;
        }
        public Device Modify(Device data)
        {
            iotContext.Update(data);
            iotContext.SaveChanges();
            return data;
        }
        public bool Delete(Device data)
        {
            iotContext.Remove(data);
            iotContext.SaveChanges();
            return true;
        }
        public DeviceResponse InsertByDto(DeviceResponse device)
        {
            Device tempValue = new Device()
            {
                Name = device.Name,
                Type = device.Type,
                Uid = device.Uid
            };
            Device insertedValue = Insert(tempValue);
            if (insertedValue == null)
                throw new System.Exception();
            return mapToDto(insertedValue);
        }
        public DeviceResponse Get(string key)
        {
            if (iotContext == null)
                return null;
            DeviceResponse foundValue = iotContext.Device
                .AsNoTracking()
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
                .AsNoTracking()
                .Select(_ => new DeviceResponse()
                {
                    Uid = _.Uid,
                    Name = _.Name,
                    Type = _.Type
                })
                .ToList();
            return foundValue;
        }
        public DeviceResponse mapToDto(Device device)
        {
            return new DeviceResponse()
            {
                Name = device.Name,
                Type = device.Type,
                Uid = device.Uid
            };
        }
    }
}
