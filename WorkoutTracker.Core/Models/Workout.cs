namespace WorkoutTracker.Core.Models{

    public class Workout{
        public string ID {get; set;}
        public int StartTime {get; set;}

        public required string Name {get; set;}

        public string? Note {get;set;}

        //public int ProgramID {get; set;}
    }

}