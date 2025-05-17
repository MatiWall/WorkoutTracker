// filepath: c:\Users\MatiW\Documents\projects\WorkoutTracker\DataBase\Controllers\WeatherController.cs
using Microsoft.AspNetCore.Mvc;

namespace WorkoutTracker.Controllers {
    
[ApiController]
[Route("/data")]
public class WorkoutDataControllers : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { Message = "Hello, API!" });
    }

    [HttpPost]
    public IActionResult UploadFile([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        // Process the uploaded file
        return Ok(new { Message = "File uploaded successfully!" });
    }
}


}
