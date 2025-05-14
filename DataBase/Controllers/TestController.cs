// filepath: c:\Users\MatiW\Documents\projects\WorkoutTracker\DataBase\Controllers\WeatherController.cs
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/data")]
public class WeatherController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { Message = "Hello, API!" });
    }
}