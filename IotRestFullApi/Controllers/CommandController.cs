using IotRestFullApi.Dto;
using IotCommon.Entities;
using IotRestFullApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace IotRestFullApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommandController : Controller
    {
        private readonly CommandRepository commandRepository;

        public CommandController(CommandRepository commandRepository)
        {
            this.commandRepository = commandRepository;
        }

        [HttpGet]
        public ActionResult GetMany()
        {
            IList<CommandResponse> response = commandRepository.GetAll();
            if (response != null)
                return Ok(response);
            else
                return StatusCode(500);
        }
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            if (id == 0)
                return BadRequest();

            CommandResponse response = commandRepository.Get(id);
            if (response != null)
                return Ok(response);
            else
                return StatusCode(500);
        }
        [HttpGet("GetLastToExecute/{DeviceId}")]
        public ActionResult GetLastToExecute(string DeviceId)
        {
            if(DeviceId.Length == 0)
                return StatusCode(500);

            CommandResponse response = commandRepository
                .GetAll()
                .Where(_ => _.Status == IotCommon.Entities.Enum.CommandStatus.ToExecute && _.DeviceID == DeviceId)
                .FirstOrDefault();
            if (response != null)
            {
                //set executed
                Command responseUpdate = new Command()
                {
                    Id = response.Id,
                    Status = IotCommon.Entities.Enum.CommandStatus.Executed,
                    Payload = response.Payload,
                    Time = response.Time,
                    Uid = response.Uid
                };
                commandRepository.Modify(responseUpdate);
                return Ok(response);
            }
            else
                return NotFound();
        }
        [HttpPut("Create")]
        public ActionResult Create([FromBody] Command command)
        {
            try
            {
                Command result = commandRepository.Insert(command);
                if (result != null)
                    return Ok(command);
                else
                    return StatusCode(500);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("Edit")]
        public ActionResult Edit([FromBody] Command command)
        {
            try
            {
                Command result = commandRepository.Modify(command);
                if (result != null)
                    return Ok(command);
                else
                    return StatusCode(500);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                bool result = commandRepository.Delete(new Command() { Id = id });
                if (result)
                    return Ok();
                else
                    return StatusCode(500);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
