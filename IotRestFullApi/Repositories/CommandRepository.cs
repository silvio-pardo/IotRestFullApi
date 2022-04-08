using IotRestFullApi.Dal;
using IotCommon.Dto;
using System.Collections.Generic;
using System.Linq;
using IotRestFullApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace IotRestFullApi.Repositories
{
    public class CommandRepository : BaseRepositories<Command>
    {
        private readonly DeviceRepository deviceRepository;

        public CommandRepository(IotContext iotContext, DeviceRepository deviceRepository) : base(iotContext)
        {
            this.deviceRepository = deviceRepository;
        }
        public CommandResponse Get(int key)
        {
            if (iotContext == null)
                return null;
            Command foundValue = iotContext.Command
                .AsNoTracking()
                .Where(_ => _.Id == key)
                .FirstOrDefault();
            return foundValue != null ? mapToDto(foundValue) : null;
        }
        public IList<CommandResponse> GetAll()
        {
            if (iotContext == null)
                return null;
            IList<CommandResponse> foundValue = iotContext.Command
                .AsNoTracking()
                .Select(_ => mapToDto(_))
                .ToList();
            return foundValue;
        }
        public CommandResponse InsertByDto(CommandResponse command)
        {
            Entities.Device deviceFound = deviceRepository.Single(command.DeviceID);
            if (deviceFound == null)
                throw new System.Exception();

            Command tempValue = new Command()
            {
                Id = command.Id,
                Payload = command.Payload,
                Device = deviceFound,
                Uid = command.Uid,
                Time = command.Time,
                Status = command.Status
            };
            Command insertedValue = Insert(tempValue);
            if (insertedValue == null)
                throw new System.Exception();
            return mapToDto(insertedValue);
        }
        public CommandResponse mapToDto(Command _)
        {
            return new CommandResponse()
            {
                Id = _.Id,
                Payload = _.Payload,
                DeviceID = _.DeviceId,
                Uid = _.Uid,
                Time = _.Time,
                Status = _.Status
            };
        }
    }
}
