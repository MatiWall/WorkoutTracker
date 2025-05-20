
using WorkoutTracker.Core.Parsers;


namespace WorkoutTracker.Core.DTO;

public class WorkoutMetadata
{
    public string programName;
    public string workoutName;
    public DateTime date;
    public WorkoutMetadata(string programName, string workoutName, DateTime date)
    {
        this.programName = programName;
        this.workoutName = workoutName;
        this.date = date;
    }

    public static WorkoutMetadata? ParseMetadata(string metadata)
    {
        var colonIndex = metadata.IndexOf(':');
        if (colonIndex == -1)
            return null; // TODO: add logging
        string programName = metadata.Substring(0, colonIndex).Trim();


        string rest = metadata.Substring(colonIndex + 1).Trim();

        var parts = rest.Split(',');


        var workoutName = parts[0];
        var date = DateTime.Parse(parts[1].Trim());


        return new WorkoutMetadata(programName, workoutName, date);
    }
}
public class Workout
{

    public WorkoutMetadata Metadata { get; set; }
    public Set[]? Sets { get; set; } = Array.Empty<Set>();

    public Workout(WorkoutMetadata Metadata, Set[] Sets)
    {
        this.Metadata = Metadata;
        this.Sets = Sets;
    }

}
