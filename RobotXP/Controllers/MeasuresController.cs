
using Microsoft.AspNetCore.Mvc;
using RobotXP.Models;
using RobotXP.Repositories;

namespace RobotXP.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class MeasuresController : Controller {
        private RobotXPRepository Repository = new();

        [HttpGet]
        public IActionResult Get([FromQuery] int? skip, int? take) {
            return Ok(Repository.GetMeasures(skip ?? 0, take ?? int.MaxValue));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id) {
            var measure = Repository.GetMeasure(id);
            if (measure == null) return NotFound();
            return Ok(measure);
        }

        [HttpGet("steps/{steps}")]
        public IActionResult GetBySteps(int steps) {
            return Ok(Repository.GetMeasuresBySteps(steps));
        }

        [HttpGet("{startDate}/{endDate}")]
        public IActionResult Get(DateTime startDate, DateTime endDate, [FromQuery] int? steps) {
            var measures = Repository.GetMeasuresFromDatePeriod(startDate, endDate, steps);
            return Ok(measures);
        }

        [HttpGet("trajectory/{trajectoryId}")]
        public IActionResult GetByTrajectory(int trajectoryId, [FromQuery] int? steps) {
            var trajectory = Repository.GetTrajectory(trajectoryId);
            if (trajectory == null) return NotFound();
            var measures = Repository.GetMeasuresFromTrajectory(trajectory, steps);
            return Ok(measures);
        }

        [HttpPost]
        public IActionResult Post([FromBody] MeasureModel measure) {
            return Ok(Repository.PostMeasure(measure));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] MeasureModel measure) {
            var newMeasure = Repository.PutMeasure(id, measure);
            if (newMeasure == null) return NotFound();
            return Ok(newMeasure);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            if (!Repository.DeleteMeasure(id)) return NotFound();
            return Ok();
        }
    }
}