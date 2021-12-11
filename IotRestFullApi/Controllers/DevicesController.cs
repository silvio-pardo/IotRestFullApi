using IotRestFullApi.Dto;
using IotRestFullApi.Entities;
using IotRestFullApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace IotRestFullApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevicesController : Controller
    {
        private readonly DeviceRepository deviceRepository;
        private readonly ILogger<DevicesController> logger;

        public DevicesController(DeviceRepository deviceRepository, ILogger<DevicesController> logger)
        {
            this.deviceRepository = deviceRepository;
            this.logger = logger;
            logger.LogInformation("Load Devices Controller");
        }

        [HttpGet]
        public ActionResult GetMany()
        {
            logger.LogInformation("Device Get Many");
            IList<DeviceResponse> response = deviceRepository.GetAll();
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

            DeviceResponse response = deviceRepository.Get(id);
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
                bool result = deviceRepository.Delete(new Device() { Uid = id });
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
