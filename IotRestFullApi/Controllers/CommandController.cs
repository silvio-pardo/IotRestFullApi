using IotRestFullApi.Entities;
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
            IList<Command> response = commandRepository.GetAll();
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

            Command response = commandRepository.Get(id);
            if (response != null)
                return Ok(response);
            else
                return StatusCode(500);
        }
        [HttpGet("GetLastToExecute")]
        public ActionResult GetLastToExecute()
        {
            Command response = commandRepository.GetAll().Where(_ => _.Status == Entities.Enum.CommandStatus.ToExecute).FirstOrDefault();
            if (response != null)
            {
                //set executed
                Command responseUpdate = response;
                responseUpdate.Status = Entities.Enum.CommandStatus.Executed;
                commandRepository.Modify(responseUpdate);
                return Ok(response);
            }
            else
                return StatusCode(500);
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
                bool result = commandRepository.Delete(id);
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
