namespace WorkoutTracker.Core.Entities;

public class Workout{
    public int ID {get; set;}
    public DateTime StartTime {get; set;}

    public required string Name {get; set;}

    public string? Note {get;set;}

    //public int ProgramID {get; set;}
}
