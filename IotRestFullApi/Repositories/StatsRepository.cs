using IotRestFullApi.Dal;
using System.Collections.Generic;
using System.Linq;
using IotCommon.Dto;
using IotRestFullApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace IotRestFullApi.Repositories
{
    public class StatsRepository : BaseRepositories<Stats>
    {
        private readonly DeviceRepository deviceRepository;

        public StatsRepository(IotContext iotContext, DeviceRepository deviceRepository) : base(iotContext)
        {
            this.deviceRepository = deviceRepository;
        }
        public StatsResponse Get(int key)
        {
            if (iotContext == null)
                return null;
            StatsResponse foundValue = iotContext.Stats
                .AsNoTracking()
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
              .AsNoTracking()
              .Select(_ => new StatsResponse() { Id = _.Id, Payload = _.Payload, DeviceID = _.DeviceId, LastUpdate = _.LastUpdate })
              .ToList(); 
            return foundValue;
        }
        public StatsResponse InsertByDto(StatsResponse stats)
        {
            Entities.Device deviceFound = deviceRepository.Single(stats.DeviceID);
            if (deviceFound == null)
                throw new System.Exception();

            Stats tempValue = new Stats()
            {
                Id = stats.Id,
                Payload = stats.Payload,
                Device = deviceFound,
                LastUpdate = stats.LastUpdate
            };
            Stats insertedValue = Insert(tempValue);
            if (insertedValue == null)
                throw new System.Exception();
            return mapToDto(insertedValue);
        }
        public StatsResponse mapToDto(Stats stats)
        {
            return new StatsResponse()
            {
                Id = stats.Id,
                Payload = stats.Payload,
                DeviceID = stats.DeviceId,
                LastUpdate = stats.LastUpdate
            };
        }
    }
}
