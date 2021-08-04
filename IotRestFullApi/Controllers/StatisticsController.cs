using IotRestFullApi.Entities;
using IotRestFullApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

        [HttpGet("GetMany")]
        public ActionResult GetMany()
        {
            IList<Stats> response = statsRepository.GetAll();
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

            Stats response = statsRepository.Get(id);
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
        [HttpDelete("Delete/{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                bool result = statsRepository.Delete(id);
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
