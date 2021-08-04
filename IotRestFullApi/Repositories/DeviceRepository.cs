﻿using IotRestFullApi.Dal;
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
        public Device Insert(Device data)
        {
            if (data == null)
                return null;
            iotContext.Add<Device>(data);
            iotContext.SaveChanges();
            return data;
        }
        public Device Modify(Device data)
        {
            if (data == null)
                return null;
            iotContext.Update<Device>(data);
            iotContext.SaveChanges();
            return data;
        }
        public bool Delete(string id)
        {
            if (id == null)
                return false;
            Device tempDevice = new Device() { Uid = id };
            iotContext.Remove<Device>(tempDevice);
            iotContext.SaveChanges();
            return true;
        }
    }
}
