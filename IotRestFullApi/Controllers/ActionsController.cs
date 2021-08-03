using IotRestFullApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IotRestFullApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActionsController : Controller
    {
        private readonly ActionRepository actionRepository;
        public ActionsController(ActionRepository actionRepository)
        {
            this.actionRepository = actionRepository;
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
        public ActionResult Create([FromBody] Action action)
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
        [HttpPost("Edit")]
        public ActionResult Edit([FromBody] Action action)
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
