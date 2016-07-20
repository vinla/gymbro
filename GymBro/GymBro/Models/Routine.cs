using System;
using SQLite;

namespace GymBro.Models
{
    public class Routine : EntityBase
    {
        [Ignore]
        public Person Person { get; set; }

        [Ignore]
        public Exercise Exercise { get; set; }

        public Int32 PersonId { get; set; }
        public Int32 ExerciseId { get; set; }
        public DateTime PerformedOn { get; set; }
        public Int32 NumberOfSets { get; set; }
        public Int32 NumberOfReps { get; set; }
        public Single WeightInKilos { get; set; }
    }
}