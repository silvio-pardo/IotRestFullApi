using IotRestFullApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using IotCommon.Dto;
using IotRestFullApi.Entities;
using System;
using Microsoft.Extensions.Logging;

namespace IotRestFullApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommandController : Controller
    {
        private readonly CommandRepository commandRepository;
        private readonly ILogger<CommandController> logger;

        public CommandController(CommandRepository commandRepository, ILogger<CommandController> logger)
        {
            this.commandRepository = commandRepository;
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult<CommandResponse> GetMany()
        {
            IList<CommandResponse> response = commandRepository.GetAll();
            if (response != null)
                return Ok(response);
            else
                return StatusCode(500);
        }
        [HttpGet("{id}")]
        public ActionResult<CommandResponse> GetById(int id)
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
        public ActionResult<CommandResponse> GetLastToExecute(string DeviceId)
        {
            try
            {
                if (DeviceId.Length == 0)
                    return StatusCode(500);

                CommandResponse response = commandRepository
                    .GetAll()
                    .Where(_ => _.Status == IotCommon.Entities.Enum.CommandStatus.ToExecute && _.DeviceID == DeviceId)
                    .FirstOrDefault();
                if (response != null)
                {
                    //set executed
                    Command finded = commandRepository.Single(response.Id);
                    if (finded == null)
                        return NotFound();

                    finded.Status = IotCommon.Entities.Enum.CommandStatus.Executed;
                    Command result = commandRepository.Modify(finded);
                    if (result == null)
                        return NotFound();

                    return Ok(commandRepository.mapToDto(result));
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest();
            }
        }
        [HttpPut("Create")]
        public ActionResult<CommandResponse> Create([FromBody] CommandResponse command)
        {
            try
            {
                CommandResponse result = commandRepository.InsertByDto(command);
                if (result != null)
                    return Ok(command);
                else
                    return StatusCode(500);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest();
            }
        }
        [HttpPost("Edit")]
        public ActionResult<CommandResponse> Edit([FromBody] CommandResponse command)
        {
            try
            {
                Command finded = commandRepository.Single(command.Id);
                if (finded == null)
                    throw new Exception();
                //update value
                finded.Status = command.Status;
                finded.Time = command.Time;
                finded.Payload = command.Payload;
                finded.Device.Uid = command.DeviceID;
                //modify
                Command result = commandRepository.Modify(finded);
                if (result == null)
                    throw new Exception();
                return Ok(commandRepository.mapToDto(result));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                Command finded = commandRepository.Single(id);
                if (finded == null)
                    throw new Exception();
                bool result = commandRepository.Delete(finded);
                if (!result)
                    throw new Exception();
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest();
            }
        }
    }
}
