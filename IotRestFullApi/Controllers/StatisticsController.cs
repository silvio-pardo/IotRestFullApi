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
    public class StatisticsController : Controller
    {
        private readonly StatsRepository statsRepository;
        private readonly ILogger<StatisticsController> logger;

        public StatisticsController(StatsRepository statsRepository, ILogger<StatisticsController> logger)
        {
            this.statsRepository = statsRepository;
            this.logger = logger;
            logger.LogInformation("Load Stats Controller");
        }

        [HttpGet]
        public ActionResult<StatsResponse> GetMany()
        {
            IList<StatsResponse> response = statsRepository.GetAll();
            if (response != null)
                return Ok(response);
            else
                return StatusCode(500);
        }
        [HttpGet("{id}")]
        public ActionResult<StatsResponse> GetById(int id)
        {
            if (id == 0)
                return BadRequest();

            StatsResponse response = statsRepository.Get(id);
            if (response != null)
                return Ok(response);
            else
                return StatusCode(500);
        }
        [HttpGet("GetLastStats")]
        public ActionResult<StatsResponse> GetLastStats()
        {
            StatsResponse response = statsRepository.GetAll().LastOrDefault();
            if (response != null)
                return Ok(response);
            else
                return StatusCode(500);
        }
        [HttpPut("Create")]
        public ActionResult<StatsResponse> Create([FromBody] StatsResponse stats)
        {
            try
            {
                StatsResponse result = statsRepository.InsertByDto(stats);
                if (result != null)
                    return Ok(result);
                else
                    return StatusCode(500);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest();
            }
        }
        [HttpPut("CreateNow")]
        public ActionResult<StatsResponse> CreateNow([FromBody] StatsResponse stats)
        {
            try
            {
                stats.LastUpdate = System.DateTime.Now;
                StatsResponse result = statsRepository.InsertByDto(stats);
                if (result != null)
                    return Ok(result);
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
        public ActionResult Edit([FromBody] StatsResponse stats)
        {
            try
            {
                Stats finded = statsRepository.Single(stats.Id);
                if (finded == null)
                    throw new Exception();
                //update value
                finded.Payload = stats.Payload;
                finded.LastUpdate = stats.LastUpdate;
                finded.Device.Uid = stats.DeviceID;
                //modify
                Stats result = statsRepository.Modify(finded);
                if (result == null)
                    throw new Exception();
                return Ok(statsRepository.mapToDto(result));
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
                Stats finded = statsRepository.Single(id);
                if (finded == null)
                    throw new Exception();
                bool result = statsRepository.Delete(finded);
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
