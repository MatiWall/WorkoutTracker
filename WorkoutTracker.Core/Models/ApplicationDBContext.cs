using Microsoft.EntityFrameworkCore;

using WorkoutTracker.Core.Models;

namespace WorkoutTracker.DataBase.Models
{
    public class ApplicationDBContext : DbContext
    {
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=192.168.0.11;Port=5432;Database=Workout;Username=postgres;Password=postgres");
        }
        */
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Set> FactSet { get; set; }
        public DbSet<DimProgram> DimProgram { get; set; }
        public DbSet<Workout> FactWorkout { get; set; }
        public DbSet<DimExercise> DimExercise { get; set; }
        public DbSet<DimMuscleGroup> DimMuscleGroup { get; set; }
    }
}