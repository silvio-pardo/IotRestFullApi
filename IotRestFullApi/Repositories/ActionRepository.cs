using System.Collections.Generic;
using System.Linq;
using IotRestFullApi.Dal;
using IotCommon.Dto;
using IotRestFullApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace IotRestFullApi.Repositories
{
    public class ActionRepository : BaseRepositories<Action>
    {
        private readonly DeviceRepository deviceRepository;

        public ActionRepository(IotContext iotContext, DeviceRepository deviceRepository) : base(iotContext)
        {
            this.deviceRepository = deviceRepository;
        }
        public ActionResponse Get(int key)
        {
            if (iotContext == null)
                return null;
            Action foundValue = iotContext.Action
                .AsNoTracking()
                .Where(_ => _.Id == key)
                .FirstOrDefault();
            return foundValue != null ? mapToDto(foundValue) : null;
        }
        public IList<ActionResponse> GetAll()
        {
            if (iotContext == null)
                return null;
            IList<ActionResponse> foundValue = iotContext.Action
              .AsNoTracking()
              .Select(_ =>  mapToDto(_))
              .ToList();
            return foundValue;
        }
        public ActionResponse InsertByDto(ActionResponse action)
        {
            Entities.Device deviceFound = deviceRepository.Single(action.DeviceID);
            if (deviceFound == null)
                throw new System.Exception();

            Action tempValue = new Action()
            {
                Id = action.Id,
                Payload = action.Payload,
                Device = deviceFound,
                Uid = action.Uid
            };
            Action insertedValue = Insert(tempValue);
            if (insertedValue == null)
                throw new System.Exception();
            return mapToDto(insertedValue);
        }
        public ActionResponse mapToDto(Action action)
        {
            return new ActionResponse()
            {
                Id = action.Id,
                Payload = action.Payload,
                DeviceID = action.DeviceId,
                Uid = action.Uid
            };
        }
    }
}
