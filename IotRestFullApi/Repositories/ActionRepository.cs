using System.Collections.Generic;
using System.Linq;
using IotRestFullApi.Dal;
using IotRestFullApi.Dto;
using IotCommon.Entities;
using Microsoft.EntityFrameworkCore;

namespace IotRestFullApi.Repositories
{
    public class ActionRepository : BaseRepositories<Action>
    {
        public ActionRepository(IotContext iotContext) : base(iotContext)
        {
        }
        public ActionResponse Get(int key)
        {
            if (iotContext == null)
                return null;
            ActionResponse foundValue = iotContext.Action
            .Where(_ => _.Id == key)
            .Select(_ => new ActionResponse() { Id = _.Id, Payload = _.Payload, DeviceID = _.DeviceId, Uid = _.Uid})
            .ToList()
            .FirstOrDefault();
            return foundValue;
        }
        public IList<ActionResponse> GetAll()
        {
            if (iotContext == null)
                return null;
            IList<ActionResponse> foundValue = iotContext.Action
              .Select(_ => new ActionResponse() { Id = _.Id, Payload = _.Payload, DeviceID = _.DeviceId, Uid = _.Uid})
              .ToList();
            return foundValue;
        }
    }
}
