using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RobotXP.Models {
    public class MeasureModel {
        [Key]
        public int Id { get; set; }

        public bool IsMoving { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        [NotMapped]
        public DateTime? EndAt { get; set; } = null;

        public ICollection<JointModel> Joints { get; set; } = new List<JointModel>();

        public double TotalPower {
            get {
                double totalPower = 0;
                foreach (JointModel joint in Joints) {
                    totalPower += joint.Power;
                }
                return totalPower;
            }
        }
    }

    
}

