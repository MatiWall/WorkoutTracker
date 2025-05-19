using System;
using System.Runtime.CompilerServices;
using System.Text;
using WorkoutTracker.Core.DTO;

namespace WorkoutTracker.Core.Parsers;

public interface IWorkoutDataParser
{
    public Workout[] ParseWorkoutData(Stream fileSteam);
}

public class WorkoutDataParser : IWorkoutDataParser
{
   
    public Workout[] ParseWorkoutData(Stream fileStream)
    {   

        string[] workouts = FindWorkouts(fileStream);

        Workout[] Workouts = Array.Empty<Workout>();

        foreach (var workout in workouts)
        {
            Workout parsedWorkout = ParseWorkout(workout);
         
        }

        return Workouts;
    }

    internal Workout ParseWorkout(string workout)
    {

        var (metadata, sets) = SplitWorkout(workout);

        (string ProgramName, string WorkoutName, DateTime Date) = ParseWorkoutMetadata(metadata);

        Workout workoutParsed = new(
            ProgramName: ProgramName,
            WorkoutName: WorkoutName,
            Date: Date,
            Sets: ParseSets(sets)
        );

        return workoutParsed;
    }

    internal static (string ProgramName, string WorkoutName, DateTime Date) ParseWorkoutMetadata(string metadata)
    {
        var colonIndex = metadata.IndexOf(':');
        if (colonIndex == -1)
            throw new ArgumentException("Invalid metadata format. Expected 'Program:Workout' format.");
        string programName = metadata.Substring(0, colonIndex).Trim();


        string rest = metadata.Substring(colonIndex + 1).Trim();

        var parts = rest.Split(',');


        var workoutName = parts[0];
        var date = DateTime.Parse(parts[1].Trim());

  
        return (programName, workoutName, date);
    }

    internal static (string Metadata, string Sets) SplitWorkout(string workout)
    {
        var parts = workout.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 2)
            throw new ArgumentException("Invalid workout format. Expected at least two lines.");
        string metadata = parts[0];
        string sets = string.Join(Environment.NewLine, parts.Skip(1));
        return (metadata, sets);
    }


    internal string[] FindWorkouts(Stream fileStream){

        var workouts = new StringBuilder();
        bool isInWorkouts = false;

        using (var reader = new StreamReader(fileStream))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (!isInWorkouts)
                {
                    if (line.Contains("Workouts"))
                    {
                        isInWorkouts = true;
                    }
                    continue; // Skip the header line
                }
                workouts.Append(line);
             
            }
        }
        return SplitWorkouts(workouts.ToString());
    }

    internal static string[] SplitWorkouts(string workouts)
    {
        string workoutsClean = RemoveFirstLine(workouts);
        return workoutsClean.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
    }

    internal static string RemoveFirstLine(string input)
    {
        var lines = input.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);
        if (lines.Length <= 1)
            return string.Empty;
        return string.Join(Environment.NewLine, lines.Skip(1));
    }


    internal static Set[] ParseSets(string sets)
    {
        var setLines = sets.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        var parsedSets = new List<Set>();
        foreach (var line in setLines)
        {
            var parts = line.Split(',');
            if (parts.Length < 4)
                throw new ArgumentException("Invalid set format. Expected 'ExerciseName,SetNR,Repetitions,Weight' format.");
            string exerciseName = parts[0].Trim();
            int setNR = int.Parse(parts[1].Trim());
            int repetitions = int.Parse(parts[2].Trim());
            float weight = float.Parse(parts[3].Trim());
            parsedSets.Add(new Set(exerciseName, setNR, repetitions, weight));
        }
        return parsedSets.ToArray();
    }
}
