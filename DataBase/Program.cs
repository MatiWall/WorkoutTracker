using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;
using Models = DataBase.Models;

/*
class Program {
    
    static void Main(){
        using (var context = new ApplicationDBContext()){
            context.Database.EnsureCreated();

            var exercise = new Models.DimExercise{
               Name="Standing Biceps Curls" 
            };

            context.DimExercise.Add(exercise);

            context.SaveChanges();

            var allExercises = context.DimExercise.ToList();

            foreach(var ex in allExercises){

                Console.WriteLine($"ID {ex.ID} and Name {ex.Name}");
            }

            var entities = context.Model.GetEntityTypes();
            foreach (var entityType in entities){
                 Console.WriteLine($"{entityType.GetTableName()}");
            }
        }
    }
};

*/

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseNpgsql("Host=192.168.0.11;Port=5432;Database=Workout;Username=postgres;Password=postgres"));
builder.Services.AddOpenApi();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try{
        Console.WriteLine("Creating database");
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
        //context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        var exercise = new Models.DimExercise{
               Name="Standing Biceps Curls" 
            };

        context.DimExercise.Add(exercise);
    
        context.SaveChanges();

        var allExercises = context.DimExercise.ToList();

        foreach(var ex in allExercises){

            Console.WriteLine($"ID {ex.ID} and Name {ex.Name}");
        }

        var entities = context.Model.GetEntityTypes();
        foreach (var entityType in entities){
                Console.WriteLine($"{entityType.GetTableName()}");
        }
        
        Console.WriteLine("Database created");
    }
    catch (Exception ex){
        Console.WriteLine($"Error creating database: {ex.Message}");
        throw;
    }
    finally{
        // Dispose of the scope
        scope.Dispose();
    }

}

if (app.Environment.IsDevelopment())
{

    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "My API V1");
        options.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();