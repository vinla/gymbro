using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymBro.Models;

namespace GymBro.ViewModels
{
    public class RoutineDataViewModel
    {
        private readonly Data.ExerciseService _exerciseService;
        private readonly Core.NavigationManager _navigationManager;
        private readonly Models.Exercise _exercise;
        private readonly Models.Person _person;
        private readonly List<Models.Routine> _routines;

        public RoutineDataViewModel(Core.NavigationManager navigationManager, Data.ExerciseService exerciseService, Models.Exercise exercise)
        {
            _navigationManager = navigationManager;
            _exerciseService = exerciseService;
            _exercise = exercise;
            _person = new Person
            {
                Id = 1,
                DisplayName = "Vincent",
                FullName = "Vincent Crowe",
                Initials = "VC",
                ColorCode = "#ffffff"
            };

            _routines = new List<Routine>();
            _routines.Add(new Routine
            {
                Exercise = _exercise,
                Person = _person,
                PerformedOn = new DateTime(2016, 4, 15),
                NumberOfReps = 12,
                NumberOfSets = 4,
                WeightInKilos = 25
            });
            _routines.Add(new Routine
            {
                Exercise = _exercise,
                Person = _person,
                PerformedOn = new DateTime(2016, 4, 23),
                NumberOfReps = 12,
                NumberOfSets = 4,
                WeightInKilos = 25
            });
            _routines.Add(new Routine
            {
                Exercise = _exercise,
                Person = _person,
                PerformedOn = new DateTime(2016, 5, 6),
                NumberOfReps = 10,
                NumberOfSets = 4,
                WeightInKilos = 30
            });
            _routines.Add(new Routine
            {
                Exercise = _exercise,
                Person = _person,
                PerformedOn = new DateTime(2016, 5, 15),
                NumberOfReps = 12,
                NumberOfSets = 4,
                WeightInKilos = 30
            });
            _routines.Add(new Routine
            {
                Exercise = _exercise,
                Person = _person,
                PerformedOn = new DateTime(2016, 5, 22),
                NumberOfReps = 10,
                NumberOfSets = 5,
                WeightInKilos = 35
            });
        }

        public String PersonName
        {
            get { return _person.DisplayName; }            
        }

        public String ExerciseName
        {
            get { return _exercise.Name; }
        }

        public List<Routine> Data
        {
            get { return _routines.OrderByDescending(d => d.PerformedOn).ToList(); }
        }
    }
}
