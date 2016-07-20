using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymBro.Models
{
    public class Exercise : EntityBase
    {        
        public String Name { get; set; }
        public ExerciseCategory Category { get; set; }
    }

    public enum ExerciseCategory
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
}
