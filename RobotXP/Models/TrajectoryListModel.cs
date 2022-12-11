using System;
namespace RobotXP.Models {
    public class TrajectoryListModel {
        public List<TrajectoryModel> Trajectories { get; set; }

        public int TotalCount { get; set; }
        public int Count { get { return Trajectories.Count(); } }

        public TrajectoryListModel(List<TrajectoryModel> Trajectories, int TotalCount) {
            this.Trajectories = Trajectories;
            this.TotalCount = TotalCount;
        }
    }
}

