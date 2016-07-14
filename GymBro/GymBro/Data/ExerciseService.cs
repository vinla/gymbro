using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using GymBro.Models;

namespace GymBro.Data
{
    public class ExerciseService
    {
        private readonly List<Person> _persons;
        private readonly List<Exercise> _exercises;
        private readonly List<Routine> _routines;

        public ExerciseService(SQLiteConnection connection)
        {
            _persons = new List<Person>();
            _persons.Add(new Person { Id = 1, FullName = "Kim Crowe", DisplayName = "Kim", Initials = "KC", ColorCode = "#C120FF" });
            _persons.Add(new Person { Id = 1, FullName = "Vincent Crowe", DisplayName = "Vin", Initials = "VC", ColorCode = "#237EFF" });

            _exercises = new List<Exercise>();
            _exercises.Add(new Exercise { Id = 1, Name = "Chest Press (Smith Machine)", Category = ExerciseCategory.Chest });
            _exercises.Add(new Exercise { Id = 2, Name = "Reverse Fly (Fly Machine)", Category = ExerciseCategory.Chest });
            _exercises.Add(new Exercise { Id = 3, Name = "V Rasie (Dumb Bells)", Category = ExerciseCategory.Chest });
            _exercises.Add(new Exercise { Id = 4, Name = "Bent Row (Arnold Machine)", Category = ExerciseCategory.Back });
            _exercises.Add(new Exercise { Id = 5, Name = "Overhead Tricep Extension (Cable Machine)", Category = ExerciseCategory.Arms });
            _exercises.Add(new Exercise { Id = 6, Name = "Stiff Leg Deadlift (Barbell)", Category = ExerciseCategory.Legs });
            _exercises.Add(new Exercise { Id = 7, Name = "Leg Extension", Category = ExerciseCategory.Legs });            
        }

        public IEnumerable<Exercise> GetExercises()
        {
            return _exercises;            
        }

        public void AddExercise(Exercise exercise)
        {
            _exercises.Add(exercise);
        }

        public void DeleteExercise(Exercise exercise)
        {
            _exercises.RemoveAll(ex => ex.Id == exercise.Id);
        }

        public void UpdateExercise(Int32 exerciseId, String name, ExerciseCategory category)
        {
            var exercise = _exercises.Single(ex => ex.Id == exerciseId);
            exercise.Name = name;
            exercise.Category = category;
        }

        public List<Routine> GetRoutines(Int32 exerciseId)
        {
            //return _routines.Where(r => r.Exercise.Id == exerciseId).ToList();

            var result = new List<Routine>();
            result.Add(new Routine { Person = _persons.First(), PerformedOn = DateTime.Now.AddDays(-2), NumberOfReps = 12, NumberOfSets = 4, WeightInKilos = 25 });
            result.Add(new Routine { Person = _persons.Last(), PerformedOn = DateTime.Now.AddDays(-2), NumberOfReps = 12, NumberOfSets = 4, WeightInKilos = 25 });
            return result;
        }
    }
}
