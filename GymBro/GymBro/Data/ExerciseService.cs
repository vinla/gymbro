using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using GymBro.Models;

namespace GymBro.Data
{
    public class ExerciseService
    {
        private readonly SQLiteConnection _connection;
        public ExerciseService(SQLiteConnection connection)
        {
            if(connection == null)         
                throw new ArgumentNullException("connection");

            _connection = connection;
        }

        public IEnumerable<Person> GetPersons()
        {
            return _connection.Table<Person>();
        }

        public IEnumerable<Exercise> GetExercises()
        {
            return _connection.Query<Exercise>("select * from Exercise").ToList();
        }

        public void AddExercise(Exercise exercise)
        {
            _connection.Insert(exercise);
        }

        public void DeleteExercise(Int32 exerciseId)
        {
            _connection.Execute("delete from Routine where ExerciseId = ?", exerciseId);
            _connection.Execute("delete from Exercise where Id = ?", exerciseId);
        }

        public void UpdateExercise(Int32 exerciseId, String name, ExerciseCategory category)
        {
            _connection.Execute("update Exercise set Name = ?, Category = ? where Id = ?", name, (byte) category, exerciseId);
        }

        public List<Routine> GetRoutines(Int32 exerciseId)
        {
            var routines = _connection.Query<Routine>("select * from Routine where ExerciseId = ?", exerciseId);
            var persons = _connection.Table<Person>().ToList();
            var exercise = _connection.Table<Exercise>().ToList().Single(ex => ex.Id == exerciseId);
            foreach (var routine in routines)
            {
                routine.Exercise = exercise;
                routine.Person = persons.Single(p => p.Id == routine.PersonId);
            }
            return routines;          
        }

        public List<Routine> GetRoutines(Int32 exerciseId, Int32 personId)
        {
            var routines = _connection.Query<Routine>("select * from Routine where ExerciseId = ? and PersonId = ?", exerciseId, personId);
            var person = _connection.Table<Person>().ToList().Single(p => p.Id == personId);
            var exercise = _connection.Table<Exercise>().ToList().Single(ex => ex.Id == exerciseId);

            foreach (var routine in routines)
            {
                routine.Exercise = exercise;
                routine.Person = person;
            }
            return routines;
        }

        public void AddEditRoutine(Routine routine)
        {
            if (routine.Id > 0)
                _connection.Update(routine);
            else
                _connection.Insert(routine);
        }

        public void DeleteRoutine(Int32 routineId)
        {
            _connection.Execute("delete from Routine where Id = ?", routineId);
        }
    }
}
