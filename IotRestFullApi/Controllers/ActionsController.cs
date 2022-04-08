using IotRestFullApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [HttpGet]
        public ActionResult<IotCommon.Dto.ActionResponse> GetMany()
        {
            IList<IotCommon.Dto.ActionResponse> response = actionRepository.GetAll();
            if (response != null)
                return Ok(response);
            else
                return StatusCode(500);
        }
        [HttpGet("{id}")]
        public ActionResult<IotCommon.Dto.ActionResponse> GetById(int id)
        {
            if (id == 0)
                return BadRequest();

            IotCommon.Dto.ActionResponse response = actionRepository.Get(id);
            if (response != null)
                return Ok(response);
            else
                return StatusCode(500);
        }
        [HttpPut("Create")]
        public ActionResult<IotCommon.Dto.ActionResponse> Create([FromBody] IotCommon.Dto.ActionResponse action)
        {
            try
            {
                IotCommon.Dto.ActionResponse result = actionRepository.InsertByDto(action);
                if (result != null)
                    return Ok(action);
                else
                    return StatusCode(500);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost("Edit")]
        public ActionResult<IotCommon.Dto.ActionResponse> Edit([FromBody] IotCommon.Dto.ActionResponse action)
        {
            try
            {
                Entities.Action finded = actionRepository.Single(action.Id);
                if (finded == null)
                    throw new Exception();
                //update value
                finded.Device.Uid = action.DeviceID;
                finded.Payload = action.Payload;
                //modify
                Entities.Action result = actionRepository.Modify(finded);
                if (result == null)
                    throw new Exception();
                return Ok(actionRepository.mapToDto(result));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                Entities.Action finded = actionRepository.Single(id);
                if(finded == null)
                    throw new Exception();
                bool result = actionRepository.Delete(finded);
                if (!result)
                    throw new Exception();
                return Ok();
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
    }
}
