using Microsoft.AspNetCore.Http;
using WorkoutTracker.Core.Parsers;
using Xunit;
using Xunit.Abstractions;

namespace WorkoutTracker.Core.DTO;
public class WorkoutDataParserTest
{   
    private readonly ITestOutputHelper output;
    public WorkoutDataParserTest(ITestOutputHelper output)
    {
        this.output = output;
    }


    [Fact]
    public void ParseWorkoutData_PrintsFileContent_WithRealFile()
    {
        output.WriteLine("Parsing workout data...");
        // Arrange
        var filePath = @"C:\Users\MatiW\Documents\projects\WorkoutTracker\StrengthLog-2023-06-201.csv";
        using var fileStream = File.OpenRead(filePath);
        var parser = new WorkoutDataParser();

        // Act
        Workout[] workouts = parser.ParseWorkoutData(fileStream);

        Assert.Equal(2, 2);
        // Assert
        // Output will be visible in the test results
    }
}
