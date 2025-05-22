
namespace WorkoutTracker.Core.Entities;

public class Set {

        public int ID {get; set;}

        public required int WorkoutID {get; set;}
        public required int ExerciseID {get; set;}
        public required int SetNR {get; set;}
        public required int Repetitions {get; set;}

        public float Weight {get; set;} 

        public string? Note {get; set;}

        
}
