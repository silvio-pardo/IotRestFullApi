using IotRestFullApi.Entities;
using IotRestFullApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

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
            return Ok();
        }
        [HttpGet("GetById/{id}")]
        public ActionResult GetById(int id)
        {
            return Ok();
        }
        [HttpPut("Create")]
        public ActionResult Create([FromBody] Command action)
        {
            try
            {
                return Ok();
            }
            catch
            {
                return View();
            }
        }
        [HttpPost("Edit")]
        public ActionResult Edit([FromBody] Command action)
        {
            try
            {
                return Ok();
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
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
