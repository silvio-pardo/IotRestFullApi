using IotRestFullApi.Entities;
using IotRestFullApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

        [HttpGet("GetMany")]
        public ActionResult GetMany()
        {
            IList<Command> response = commandRepository.GetAll();
            if (response != null)
                return Ok(response);
            else
                return StatusCode(500);
        }
        [HttpGet("GetById/{id}")]
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
        [HttpDelete("Delete/{id}")]
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
