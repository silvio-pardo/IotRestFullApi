using IotRestFullApi.Dal;
using IotRestFullApi.Dto;
using IotRestFullApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IotRestFullApi.Repositories
{
    public class CommandRepository : BaseRepositories<Command>
    {
        public CommandRepository(IotContext iotContext) : base(iotContext)
        {
        }
        public CommandResponse Get(int key)
        {
            if (iotContext == null)
                return null;
            CommandResponse foundValue = iotContext.Command
                .Where(_ => _.Id == key)
                .Select(_ => new CommandResponse() { Id = _.Id, Payload = _.Payload, DeviceID = _.DeviceId, Uid = _.Uid, Time = _.Time, Status = _.Status })
                .ToList()
                .FirstOrDefault();
            return foundValue;
        }
        public IList<CommandResponse> GetAll()
        {
            if (iotContext == null)
                return null;
            IList<CommandResponse> foundValue = iotContext.Command
                .Select(_ => new CommandResponse() { Id = _.Id, Payload = _.Payload, DeviceID = _.DeviceId, Uid = _.Uid, Time = _.Time, Status = _.Status })
                .ToList();
            return foundValue;
        }
    }
}
