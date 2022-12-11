using Microsoft.EntityFrameworkCore;

namespace RobotXP.Models {
    public class RobotXPContext : DbContext {
        public DbSet<JointModel> Joints { get; set; } = null!;

        public DbSet<MeasureModel> Measures { get; set; } = null!;

        public DbSet<TrajectoryModel> Trajectories { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer("Server=localhost;Database=robot_xp;User=sa;Password=Mqtt@123");
        }
    }
}

