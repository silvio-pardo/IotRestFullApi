using IotRestFullApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using IotCommon.Dto;
using IotRestFullApi.Entities;
using System;

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
        public ActionResult<DeviceResponse> GetMany()
        {
            logger.LogInformation("Device Get Many");
            IList<DeviceResponse> response = deviceRepository.GetAll();
            if (response != null)
                return Ok(response);
            else
                return StatusCode(500);
        }
        [HttpGet("{id}")]
        public ActionResult<DeviceResponse> GetById(string id)
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
        public ActionResult<DeviceResponse> Create([FromBody] DeviceResponse device)
        {
            try
            {
                DeviceResponse result = deviceRepository.InsertByDto(device);
                if (result != null)
                    return Ok(result);
                else
                    return StatusCode(500);
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest();
            }
        }
        [HttpPost("Edit")]
        public ActionResult<DeviceResponse> Edit([FromBody] DeviceResponse device)
        {
            try
            {
                Device finded = deviceRepository.Single(device.Uid);
                if (finded == null)
                    throw new Exception();
                //update value
                finded.Name = device.Name;
                finded.Type = device.Type;
                //modify
                Device result = deviceRepository.Modify(finded);
                if (result == null)
                    throw new Exception();
                return Ok(deviceRepository.mapToDto(result));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                Device finded = deviceRepository.Single(id);
                if (finded == null)
                    throw new Exception();
                bool result = deviceRepository.Delete(finded);
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
