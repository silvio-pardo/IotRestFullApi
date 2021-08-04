using IotRestFullApi.Dal;
using IotRestFullApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IotRestFullApi.Repositories
{
    public class StatsRepository
    {
        private readonly IotContext iotContext;
        public StatsRepository(IotContext iotContext)
        {
            this.iotContext = iotContext;
        }
        public Stats Get(int key)
        {
            if (iotContext == null)
                return null;
            Stats foundValue = iotContext.Stats.Where(_ => _.Id == key).FirstOrDefault();
            return foundValue;
        }
        public IList<Stats> GetAll()
        {
            if (iotContext == null)
                return null;
            IList<Stats> foundValue = iotContext.Stats.Select(_ => new Stats() { Id=_.Id, Device=_.Device, Payload=_.Payload, LastUpdate=_.LastUpdate}).ToList();
            return foundValue;
        }
        public Stats Insert(Stats data)
        {
            if (data == null)
                return null;
            iotContext.Add<Stats>(data);
            iotContext.SaveChanges();
            return data;
        }
        public Stats Modify(Stats data)
        {
            if (data == null)
                return null;
            iotContext.Update<Stats>(data);
            iotContext.SaveChanges();
            return data;
        }
        public bool Delete(int id)
        {
            if (id == 0)
                return false;
            Stats tempStats = new Stats() { Id = id };
            iotContext.Remove<Stats>(tempStats);
            iotContext.SaveChanges();
            return true;
        }
    }
}
