

using Microsoft.EntityFrameworkCore;
using Models = DataBase.Models;


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
}


