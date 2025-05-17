using System;

namespace WorkoutTracker.API.Parsers;

public interface IWorkoutDataParser
{
    void ParseWorkoutData(IFormFile file);
}

public class WorkoutDataParser : IWorkoutDataParser
{
    public void ParseWorkoutData(IFormFile file)
    {
        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
               
            }
        }
    }
}
