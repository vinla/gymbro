using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymBro.Models
{
    public class Exercise : EntityBase
    {        
        private static String[] options = { "X", "A", "L", "CH", "B", "SH", "CR", "CA", "M" };

        private static String[] backgroundColors =
        {
            "Black", "Aqua", "#FFCB4B", "Maroon", "#FF530D", "#4B0CE8", "#4D8DFF",
            "#388A1D", "#4D8D9B"
        };

        private static String[] textColors =
        {
            "White", "#666666", "Black", "Silver", "Silver", "Silver", "#666666",
            "Silver", "Black"
        };


        public String Name { get; set; }
        public ExerciseCategory Category { get; set; }

        public String CategoryDisplay => options[(Int32) Category];

        public String TextColor => textColors[(Int32) Category];

        public String BackColor => backgroundColors[(Int32) Category];
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
