using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using WorkoutTracker.Core.DTO;
using WorkoutTracker.Core.Parsers;

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

        List<Workout> Workouts = new List<Workout>();

        foreach (var workout in workouts)
        {
           
            Workout parsedWorkout = ParseWorkout(workout);
            if (parsedWorkout == null)
            {
                continue;
            }
            Workouts.Add(parsedWorkout);
            
          
         
        }

        return Workouts.ToArray();
    }

    internal Workout? ParseWorkout(string workout)
    {

        var (metadata, sets) = SplitWorkout(workout);

        WorkoutMetadata? workoutMetadata = WorkoutMetadata.ParseMetadata(metadata);

        if (workoutMetadata == null)
        {
            return null;
        }

        Workout workoutParsed = new(
            Metadata: workoutMetadata,
            Sets: ParseSets(sets)
        );

        return workoutParsed;
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

        string keyword = "Workouts";

        string fileContent;

        using (var reader = new StreamReader(fileStream))
        {
            fileContent = reader.ReadToEnd();
        }

        int index = fileContent.IndexOf(keyword, StringComparison.OrdinalIgnoreCase);
        if (index == -1)
            throw new ArgumentException($"Keyword '{keyword}' not found in the file.");

        int endIndex = fileContent.IndexOf("\n", index);
        if (endIndex == -1)
            endIndex = fileContent.Length; // If no newline is found, read till the end of the file

        string workouts = fileContent.Substring(endIndex + 1);

        return SplitWorkouts(workouts.ToString());
    }

    internal static string[] SplitWorkouts(string workouts)
    {
        string workoutsClean = RemoveFirstLine(workouts);
        return workoutsClean.Split(new[] { "\r\n\r\n", "\n\n", "\r\r" }, StringSplitOptions.RemoveEmptyEntries);
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

            var parts = line.Replace("\"", "").Split(',');
            if (parts.Length < 4)
                continue;

            string exerciseName = parts[1].Trim();
            int setNR = int.Parse(parts[3].Trim());
            int repetitions = int.Parse(parts[5].Trim());
            float weight = float.Parse(parts[7].Trim());
            parsedSets.Add(new Set(exerciseName, setNR, repetitions, weight));
        }
        return parsedSets.ToArray();
    }
}
