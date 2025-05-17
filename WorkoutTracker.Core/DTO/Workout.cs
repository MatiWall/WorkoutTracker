using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WorkoutTracker.Core.Models;

namespace WorkoutTracker.Core.DTO;

internal class WorkoutContainer
{

    public Workout? Workout { get; set; }
    public Set[]? Sets { get; set; }

    public WorkoutContainer(Workout workout, Set[] sets)
    {
        Workout = workout;
        Sets = sets;
    }

}

internal class SetContainer
{
    public Set? Set { get; set; }
    public DimExercise[]? Exercises { get; set; }
    public SetContainer(Set set, DimExercise[] exercises)
    {
        Set = set;
        Exercises = exercises;
    }
}
