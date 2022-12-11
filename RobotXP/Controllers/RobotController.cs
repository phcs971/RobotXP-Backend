using Microsoft.AspNetCore.Mvc;
using RobotXP.Models;
using RobotXP.Repositories;

namespace RobotXP.Controllers {
    [Route("api/[controller]")]
    public class RobotController : Controller {
        private RobotXPRepository Repository = new();

        [HttpGet("isOn")]
        public IActionResult GetIsOn() {
            return Ok(Repository.GetIsOn());
        }
    }
}