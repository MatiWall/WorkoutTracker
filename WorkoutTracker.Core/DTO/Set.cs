using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutTracker.Core.DTO
{
    public class Set
    {
        public string ExerciseName { get; set; } = string.Empty;
        public int SetNR { get; set; }
        public int Repetitions { get; set; }

        public float Weight { get; set; }


        public Set(string ExerciseName, int SetNR, int Repetitions, float Weight) {

            this.ExerciseName = ExerciseName;
            this.SetNR = SetNR;
            this.Repetitions = Repetitions;
            this.Weight = Weight;
        }
    }
}
