using WorkoutTracker.Core.DTO;

namespace WorkoutTracker.API.Utilities
{

    public class Utils
    {
        public static string[] GerUniqueExercises(Workout[] workouts)
        {
            HashSet<string> uniqueExercises = new HashSet<string>();

            foreach (var workout in workouts)
            {
                foreach (var set in workout.Sets)
                {
                    uniqueExercises.Add(set.ExerciseName);
                }
            }

            return uniqueExercises.ToArray();
        }

    }

}