using IotRestFullApi.Dal;
using IotRestFullApi.Entities;
using System.Collections.Generic;
using System.Linq;
using IotRestFullApi.Dto;
using Microsoft.EntityFrameworkCore;

namespace IotRestFullApi.Repositories
{
    public class StatsRepository : BaseRepositories<Stats>
    {
        public StatsRepository(IotContext iotContext) : base(iotContext)
        {
        }
        public StatsResponse Get(int key)
        {
            if (iotContext == null)
                return null;
            StatsResponse foundValue = iotContext.Stats
             .Where(_ => _.Id == key)
             .Select(_ => new StatsResponse() { Id = _.Id, Payload = _.Payload, DeviceID = _.DeviceId, LastUpdate = _.LastUpdate })
             .ToList()
             .FirstOrDefault();
            return foundValue;
        }
        public IList<StatsResponse> GetAll()
        {
            if (iotContext == null)
                return null;
            IList<StatsResponse> foundValue = iotContext.Stats
              .Select(_ => new StatsResponse() { Id = _.Id, Payload = _.Payload, DeviceID = _.DeviceId, LastUpdate = _.LastUpdate })
              .ToList(); 
            return foundValue;
        }
    }
}
