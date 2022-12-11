
using Microsoft.AspNetCore.Mvc;
using RobotXP.Repositories;
using RobotXP.Models;

namespace RobotXP.Migrations {
    [ApiController]
    [Route("api/[controller]")]
    public class TrajectoriesController : Controller {
        private RobotXPRepository Repository = new();

        [HttpGet]
        public IActionResult Get([FromQuery] int? skip, int? take) {
            return Ok(Repository.GetTrajectories(skip ?? 0, take ?? int.MaxValue));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id) {
            var trajectory = Repository.GetTrajectory(id);
            if (trajectory == null) return NotFound();
            return Ok(trajectory);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TrajectoryModel trajectory) {
            return Ok(Repository.PostTrajectory(trajectory));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string trajectoryName) {
            var newTrajectory = Repository.PutTrajectory(id, trajectoryName);
            if (newTrajectory == null) return NotFound();
            return Ok(newTrajectory);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            if (!Repository.DeleteTrajectory(id)) return NotFound();
            return Ok();
        }
    }
}