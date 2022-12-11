using System;
using System.ComponentModel.DataAnnotations;

namespace RobotXP.Models {
    public class JointModel {
        [Key]
        public int Id { get; set; }

        public double Current { get; set; }

        public double Voltage { get; set; }

        public int Number { get; set; }

        public double Power { get { return Current * Voltage; } }

    }
}

