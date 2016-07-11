using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace GymBro.Models
{
    public class EntityBase
    {
        [PrimaryKey, AutoIncrement]
        public Int32 Id { get; set; }
    }

    public class Person : EntityBase
    {
        public String FullName { get; set; }
        public String DisplayName { get; set; }
        public String Initials { get; set; }
        public String ColorCode { get; set; }
    }    

    public enum ExerciseCategory : Byte
    {
        All = 0,
        Arms = 1,
        Legs = 2,
        Chest = 3,
        Back = 4,
        Shoulders = 5,
        Core = 6,
        Cardio = 7,
        Misc = 8
    }

    public class Exercise : EntityBase
    {        
        public String Name { get; set; }
        public ExerciseCategory Category { get; set; }
    }    

    public class Routine
    {
        public Person Person { get; set; }
        public Exercise Exercise { get; set; }
        public DateTime PerformedOn { get; set; }
        public Int32 NumberOfSets { get; set; }
        public Int32 NumberOfReps { get; set; }
        public Single WeightInKilos { get; set; }
    }
}
