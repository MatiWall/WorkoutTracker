namespace DataBase.Models{

    public class Workout{
        public int ID {get; set;}
        public int StartTime {get; set;}

        public int? EndTime {get;set;}
        public required string Name {get; set;}

        public string? Note {get;set;}

        public int ProgramID {get; set;}
    }

}