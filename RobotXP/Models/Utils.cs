using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobotXP.Models {
    public class Utils {
        static public MeasureModel MeasureAverage(List<MeasureModel> measures, DateTime start, DateTime end) {
            if (measures.Count() == 0) return EmptyMeasureAverage(start, end);

            MeasureModel average = new() {
                CreatedAt = start,
                UpdatedAt = measures.First().UpdatedAt,
                Id = measures.First().Id,
                IsMoving = measures.Any((m) => m.IsMoving),
                EndAt = end,
                Joints = new List<JointModel>()
            };
            foreach (JointModel joint in measures.First().Joints) {
                average.Joints.Add(new JointModel {
                    Id = joint.Id,
                    Number = joint.Number,
                    Current = (double) measures.Select(measure => measure.Joints.First(j => j.Number == joint.Number).Current).Average(),
                    Voltage = (double) measures.Select(measure => measure.Joints.First(j => j.Number == joint.Number).Voltage).Average()
                });
            }

            return average;
        }

        static public MeasureModel EmptyMeasureAverage(DateTime start, DateTime end) {
            MeasureModel average = new() {
                CreatedAt = start,
                UpdatedAt = start,
                IsMoving = false,
                EndAt = end,
                Joints = new List<JointModel>()
            };
            for (var i = 1; i <= 6; i++) {
                average.Joints.Add(new() {
                    Id = i,
                    Number = i,
                    Current = 0,
                    Voltage = 0
                });
            }

            return average;
        }
    }
}