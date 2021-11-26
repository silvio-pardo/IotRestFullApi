using IotRestFullApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using IotRestFullApi.Entities;
using IotRestFullApi.Dto;

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

        [HttpGet]
        public ActionResult GetMany()
        {
            IList<ActionResponse> response = actionRepository.GetAll();
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

            ActionResponse response = actionRepository.Get(id);
            if (response != null)
                return Ok(response);
            else
                return StatusCode(500);
        }
        [HttpPut("Create")]
        public ActionResult Create([FromBody] Action action)
        {
            try
            {
                Action result = actionRepository.Insert(action);
                if (result != null)
                    return Ok(action);
                else
                    return StatusCode(500);
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
                Action result = actionRepository.Modify(action);
                if (result != null)
                    return Ok(action);
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
                bool result = actionRepository.Delete(new Action() { Id = id });
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
