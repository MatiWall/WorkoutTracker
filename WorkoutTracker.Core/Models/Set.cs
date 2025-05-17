
namespace WorkoutTracker.Core.Models {

    public class Set {

        public string ID {get; set;}

        public required int WorkoutID {get; set;}
        public required int ExerciseID {get; set;}
        public required int Repetitions {get; set;}

        public float Weight {get; set;} 

        public string? Note {get; set;}

        
    }

}