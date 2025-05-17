using System;
using System.Runtime.CompilerServices;
using System.Text;
using WorkoutTracker.Core.DTO;
using WorkoutTracker.Core.Models;

namespace WorkoutTracker.Core.Parsers;

public interface IWorkoutDataParser
{
    void ParseWorkoutData(Stream fileSteam);
}

public class WorkoutDataParser : IWorkoutDataParser
{
   
    public void ParseWorkoutData(Stream fileStream)
    {
        string[] workouts = FindWorkouts(fileStream);

        WorkoutContainer[] workoutContainers = {};

        foreach (var workout in workouts)
        {
            WorkoutContainer parsedWorkout = ParseWorkout(workout);
         
        }


    }

    internal WorkoutContainer ParseWorkout(string workout)
    {

        var (metadata, sets) = SplitWorkout(workout);

        (string ProgramName, string WorkoutName, DateTime Date) = ParseWorkoutMetadata(metadata);



        return "string";
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

    internal string[] ParseWorkoutMetadata(string workout){
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
}
