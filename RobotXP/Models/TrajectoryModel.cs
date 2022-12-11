using System.ComponentModel.DataAnnotations;

namespace RobotXP.Models {
    public class TrajectoryModel {
        public int Id { get; set; }

        public string? Name { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime EndedAt { get; set; }
    }
}

