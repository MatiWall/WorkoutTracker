using DTO = WorkoutTracker.Core.DTO;
using WorkoutTracker.Core.Entities;
using WorkoutTracker.API.Utilities;
using System.Runtime.CompilerServices;


namespace WorkoutTracker.API.DBInserter;


public class DBInserter
{
	private readonly ApplicationDBContext _context;

	public DBInserter(IServiceProvider serviceProvider)
	{
		_context = serviceProvider.GetRequiredService<ApplicationDBContext>();
    }
    public void Insert(DTO.Workout[] workouts)
	{
		UpdateExercises(workouts);
		UpdatePrograms(workouts);


		foreach (DTO.Workout workout in workouts)
		{
			Workout workoutEntity = new Workout { 
				Name = workout.Metadata.workoutName, 
				StartTime = workout.Metadata.date.ToUniversalTime() 
			};

			_context.FactWorkout.Add(workoutEntity);

            _context.SaveChanges(); // Save the workout entity to get its ID


            foreach (DTO.Set set in workout.Sets)
			{
				var exercise = _context.DimExercise.FirstOrDefault(e => e.Name == set.ExerciseName);

				var factSet = new Set
				{
					WorkoutID = workoutEntity.ID,
					ExerciseID = exercise.ID,
					Repetitions = set.Repetitions,
					SetNR = set.SetNR,
                    Weight = set.Weight
				};

                _context.FactSet.Add(factSet);	
            }
        }
       

    }

	internal void UpdateExercises(DTO.Workout[] workouts)
	{

		string[] exercises = Utils.GerUniqueExercises(workouts);

		
        string[] existing_exercises = _context.DimExercise.Select(e => e.Name).ToArray();

		foreach (string exerciseName in exercises) {
			if (!existing_exercises.Contains(exerciseName)){
				var newExercise = new DimExercise {
					Name = exerciseName
				};
				_context.DimExercise.Add(newExercise);
			};
		

			};
        _context.SaveChanges();
	
	}

	internal void UpdatePrograms(DTO.Workout[] workouts) { 
		
		HashSet<string> uniquePrograms = new HashSet<string>();

		foreach (var workout in workouts)
		{
			uniquePrograms.Add(workout.Metadata.programName);
        }

		foreach (var programName in uniquePrograms)
		{

				var existingProgram = _context.DimProgram.FirstOrDefault(p => p.Name == programName);
				if (existingProgram == null)
				{
					var newProgram = new DimProgram
					{
						Name = programName
					};
                _context.DimProgram.Add(newProgram);
                _context.SaveChanges();
				}
           
        }
    }

	
}