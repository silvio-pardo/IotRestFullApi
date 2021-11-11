using IotRestFullApi.Entities;
using IotRestFullApi.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace IotRestFullApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevicesController : Controller
    {
        private readonly DeviceRepository deviceRepository;

        public DevicesController(DeviceRepository deviceRepository)
        {
            this.deviceRepository = deviceRepository;
        }

        [HttpGet]
        public ActionResult GetMany()
        {
            IList<Device> response = deviceRepository.GetAll();
            if (response != null)
                return Ok(response);
            else
                return StatusCode(500);
        }
        [HttpGet("{id}")]
        public ActionResult GetById(string id)
        {
            if (id == null)
                return BadRequest();

            Device response = deviceRepository.Get(id);
            if (response != null)
                return Ok(response);
            else
                return StatusCode(500);
        }
        [HttpPut("Create")]
        public ActionResult Create([FromBody] Device device)
        {
            try
            {
                Device result = deviceRepository.Insert(device);
                if (result != null)
                    return Ok(device);
                else
                    return StatusCode(500);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("Edit")]
        public ActionResult Edit([FromBody] Device device)
        {
            try
            {
                Device result = deviceRepository.Modify(device);
                if (result != null)
                    return Ok(device);
                else
                    return StatusCode(500);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                bool result = deviceRepository.Delete(id);
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
