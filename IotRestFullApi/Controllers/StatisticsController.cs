using IotRestFullApi.Dto;
using IotRestFullApi.Entities;
using IotRestFullApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace IotRestFullApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : Controller
    {
        private readonly StatsRepository statsRepository;

        public StatisticsController(StatsRepository statsRepository)
        {
            this.statsRepository = statsRepository;
        }

        [HttpGet]
        public ActionResult GetMany()
        {
            IList<StatsResponse> response = statsRepository.GetAll();
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

            StatsResponse response = statsRepository.Get(id);
            if (response != null)
                return Ok(response);
            else
                return StatusCode(500);
        }
        [HttpGet("GetLastStats")]
        public ActionResult GetLastStats()
        {
            StatsResponse response = statsRepository.GetAll().LastOrDefault();
            if (response != null)
                return Ok(response);
            else
                return StatusCode(500);
        }
        [HttpPut("Create")]
        public ActionResult Create([FromBody] Stats stats)
        {
            try
            {
                Stats result = statsRepository.Insert(stats);
                if (result != null)
                    return Ok(stats);
                else
                    return StatusCode(500);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("CreateNow")]
        public ActionResult CreateNow([FromBody] Stats stats)
        {
            try
            {
                stats.LastUpdate = System.DateTime.Now;
                Stats result = statsRepository.Insert(stats);
                if (result != null)
                    return Ok(stats);
                else
                    return StatusCode(500);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("Edit")]
        public ActionResult Edit([FromBody] Stats stats)
        {
            try
            {
                Stats result = statsRepository.Modify(stats);
                if (result != null)
                    return Ok(stats);
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
                bool result = statsRepository.Delete(new Stats() { Id = id });
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
