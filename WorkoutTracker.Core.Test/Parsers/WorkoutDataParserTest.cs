using Microsoft.AspNetCore.Http;
using WorkoutTracker.Core.Parsers;
using Xunit;
using Xunit.Abstractions;

namespace WorkoutTracker.API.UnitTest.Parsers;

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
        parser.ParseWorkoutData(fileStream);

        // Assert
        // Output will be visible in the test results
    }
}
