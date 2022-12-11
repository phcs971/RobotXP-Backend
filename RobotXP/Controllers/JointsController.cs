using Microsoft.AspNetCore.Mvc;
using RobotXP.Models;
using RobotXP.Repositories;

namespace RobotXP.Controllers {
    [Route("api/[controller]")]
    public class JointsController : Controller {
        private RobotXPRepository Repository = new();

        [HttpGet]
        public List<JointModel> Get([FromQuery] int? skip, int? take) {
            return Repository.GetJoints(skip ?? 0, take ?? int.MaxValue);
        }

        

        [HttpGet("{id}")]
        public IActionResult Get(int id) {
            var joint = Repository.GetJoint(id);
            if (joint == null) return NotFound();
            return Ok(joint);
        }

        [HttpPost]
        public IActionResult Post([FromBody] JointModel joint) {
            return Ok(Repository.PostJoint(joint));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] JointModel joint) {
            var newJoint = Repository.PutJoint(id, joint);
            if (newJoint == null) return NotFound();
            return Ok(newJoint);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            if (!Repository.DeleteJoint(id)) return NotFound();
            return Ok();
        }
    }
}