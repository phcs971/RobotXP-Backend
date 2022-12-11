
using RobotXP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RobotXP.Repositories {
    public class RobotXPRepository {
        public List<TrajectoryModel> GetTrajectories() {
            using var context = new RobotXPContext();
            return context.Trajectories.ToList();
        }

        public TrajectoryListModel GetTrajectories(int skip, int take) {
            using var context = new RobotXPContext();
            var count = context.Trajectories.Count();

            return new (context.Trajectories.OrderByDescending(t=> t.StartedAt).Skip(skip).Take(take).ToList(), count);
        }


        public TrajectoryModel? GetTrajectory(int id) {
            using var context = new RobotXPContext();
            return context.Trajectories.Find(id);
        }

        public TrajectoryModel PostTrajectory(TrajectoryModel trajectory) {
            using var context = new RobotXPContext();
            context.Trajectories.Add(trajectory);
            context.SaveChanges();
            return trajectory;
        }

        public TrajectoryModel? PutTrajectory(int id, string trajectoryName) {
            using var context = new RobotXPContext();
            var oldTrajectory = context.Trajectories.Find(id);
            if (oldTrajectory == null) return null;
            oldTrajectory.Name = trajectoryName;
            context.SaveChanges();
            return oldTrajectory;
        }

        public bool DeleteTrajectory(int id) {
            using var context = new RobotXPContext();
            var trajectory = context.Trajectories.Find(id);
            if (trajectory == null) return false;
            context.Trajectories.Remove(trajectory);
            context.SaveChanges();
            return true;
        }

        public List<JointModel> GetJoints() {
            using var context = new RobotXPContext();
            return context.Joints.ToList();
        }

        public List<JointModel> GetJoints(int skip, int take) {
            using var context = new RobotXPContext();
            return context.Joints.Skip(skip).Take(take).ToList();
        }

        public JointModel? GetJoint(int id) {
            using var context = new RobotXPContext();
            return context.Joints.Find(id);
        }

        public JointModel PostJoint(JointModel joint) {
            using var context = new RobotXPContext();
            context.Joints.Add(joint);
            context.SaveChanges();
            return joint;
        }

        public JointModel? PutJoint(int id, JointModel joint) {
            using var context = new RobotXPContext();
            var oldJoint = context.Joints.Find(id);
            if (oldJoint == null) return null;
            oldJoint.Number = joint.Number;
            oldJoint.Current = joint.Current;
            oldJoint.Voltage = joint.Voltage;
            context.SaveChanges();
            return oldJoint;
        }

        public bool DeleteJoint(int id) {
            using var context = new RobotXPContext();
            var joint = context.Joints.Find(id);
            if (joint == null) return false;
            context.Joints.Remove(joint);
            context.SaveChanges();
            return true;
        }

        public List<MeasureModel> GetMeasures() {
            using var context = new RobotXPContext();
            return context.Measures.Include(m => m.Joints).ToList();
        }

        public List<MeasureModel> GetMeasures(int skip, int take) {
            using var context = new RobotXPContext();
            return context.Measures.Include(m => m.Joints).Skip(skip).Take(take).ToList();
        }

        public List<MeasureModel> GetMeasuresBySteps(int steps) {
            using var context = new RobotXPContext();
            var start = context.Measures.OrderBy(m => m.CreatedAt).FirstOrDefault()?.CreatedAt;
            if (start == null) return new List<MeasureModel>();
            return GetMeasuresFromDatePeriod((DateTime) start, DateTime.Now, steps);
        }

        public List<MeasureModel> GetMeasuresFromDatePeriod(DateTime from, DateTime to, int? steps = null) {
            using var context = new RobotXPContext();
            if (steps == null) return context.Measures.Include(m => m.Joints).Where(m => m.CreatedAt >= from && m.CreatedAt <= to).ToList();
            var dateDiff = to - from;
            double step = dateDiff.TotalSeconds / (steps ?? 1.0);
            var m = new List<MeasureModel>();
            for (var i = 0; i < steps; i++) {
                var fromDate = from.AddSeconds(i * step);
                var toDate = fromDate.AddSeconds(step);
                var measures = context.Measures.Include(m => m.Joints).Where(m => m.CreatedAt >= fromDate && m.CreatedAt <= toDate).ToList();
                var average = Utils.MeasureAverage(measures, fromDate, toDate);
                m.Add(average);
                
            }
            return m;
        }

        public List<MeasureModel> GetMeasuresFromTrajectory(TrajectoryModel trajectory, int? steps) {
            return GetMeasuresFromDatePeriod(trajectory.StartedAt, trajectory.EndedAt, steps);
        }

        public MeasureModel? GetMeasure(int id) {
            using var context = new RobotXPContext();
            return context.Measures.Include(m => m.Joints).Where(m => m.Id == id).FirstOrDefault();
        }

        public MeasureModel PostMeasure(MeasureModel measure) {
            using var context = new RobotXPContext();
            context.Measures.Add(measure);
            context.SaveChanges();
            return measure;
        }

        public MeasureModel? PutMeasure(int id, MeasureModel measure) {
            using var context = new RobotXPContext();
            var oldMeasure = context.Measures.Find(id);
            if (oldMeasure == null) return null;
            oldMeasure.IsMoving = measure.IsMoving;
            oldMeasure.UpdatedAt = DateTime.Now;
            context.SaveChanges();
            return oldMeasure;
        }

        public bool DeleteMeasure(int id) {
            using var context = new RobotXPContext();
            var measure = context.Measures.Find(id);
            if (measure == null) return false;
            context.Measures.Remove(measure);
            context.SaveChanges();
            return true;
        }

        public bool GetIsOn() {
            using var context = new RobotXPContext();
            var measure = context.Measures.OrderByDescending(m => m.CreatedAt).FirstOrDefault();
            if (measure == null) return false;
            return measure.CreatedAt.AddMinutes(1) > DateTime.Now;
        }
    }
}

