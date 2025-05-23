// filepath: c:\Users\MatiW\Documents\projects\WorkoutTracker\DataBase\Controllers\WeatherController.cs
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.API.DBInserter;
using WorkoutTracker.Core.DTO;
using WorkoutTracker.Core.Parsers;


namespace WorkoutTracker.Controllers;


[ApiController]
[Route("/data")]
public class WorkoutDataControllers : ControllerBase
{
    private readonly DBInserter _dbInserter;

    public WorkoutDataControllers(DBInserter dbInserter)
    {
        _dbInserter = dbInserter;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { Message = "Hello, API!" });
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public IActionResult UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        WorkoutDataParser parser = new WorkoutDataParser();

        Workout[] workouts = parser.ParseWorkoutData(file.OpenReadStream());


        _dbInserter.Insert(workouts);

        // Process the uploaded file
        return Ok(new { Message = "File uploaded successfully!" });
    }
}
