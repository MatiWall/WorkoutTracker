using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WorkoutTracker.Core.DTO;

public class Workout
{

    public string ProgramName { get; set; } = string.Empty;

    public string WorkoutName { get; set; } = string.Empty;

    public DateTime Date { get; set; } = DateTime.MinValue;
    public Set[]? Sets { get; set; } = Array.Empty<Set>();

    public Workout(string ProgramName, string WorkoutName, DateTime Date, Set[] Sets)
    {
        this.ProgramName = ProgramName;
        this.WorkoutName = WorkoutName;
        this.Date = Date;
        this.Sets = Sets;
    }

}
