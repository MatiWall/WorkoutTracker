using Microsoft.EntityFrameworkCore;
using Models = DataBase.Models;

public class ApplicationDBContext : DbContext {
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("MyTestDB");
    }
    public DbSet<Models.Set> FactSet {get; set;}
    public DbSet<Models.DimProgram> DimProgram {get;set;}
    public DbSet<Models.Workout> FactWorkout {get;set;}
    public DbSet<Models.DimExercise> DimExercise {get; set;}
    public DbSet<Models.DimMuscleGroup> DimMuscleGroup {get; set;}
}