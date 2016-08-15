using System;
using GymBro.Core;
using GymBro.Data;
using GymBro.Models;

namespace GymBro.ViewModels
{
    public class AddEntryViewModel : ViewModel
    {
        private readonly NavigationManager _navigationManager;
        private readonly ExerciseService _exerciseService;
        private readonly Routine _routine;
        private readonly Exercise _exercise;
        private readonly Person _person;

        public AddEntryViewModel(NavigationManager navigationManager, ExerciseService exerciseService, Exercise exercise,
            Person person)
        {
            _navigationManager = navigationManager;
            _exerciseService = exerciseService;
            _routine = new Routine
            {
                Id = 0,
                Exercise = exercise,
                ExerciseId = exercise.Id,
                Person = person,
                PersonId = person.Id,
                NumberOfReps = 12,
                NumberOfSets = 4,
                WeightInKilos = 10,
                PerformedOn = DateTime.Today
            };            
        }

        public AddEntryViewModel(NavigationManager navigationManager, ExerciseService exerciseService, Routine existingRoutine)
        {
            _navigationManager = navigationManager;
            _exerciseService = exerciseService;
            _routine = existingRoutine;
        }

        public Exercise Exercise => _routine.Exercise;

        public Person Person => _routine.Person;

        public Int32 NumberOfReps
        {
            get { return _routine.NumberOfReps; }
            set { _routine.NumberOfReps = value; }
        }

        public Int32 NumberOfSets
        {
            get { return _routine.NumberOfSets; }
            set { _routine.NumberOfSets = value; }
        }

        public Single WeightInKilos
        {
            get { return _routine.WeightInKilos; } 
            set { _routine.WeightInKilos = value; }
        }

        public DateTime PerformedOn
        {
            get { return _routine.PerformedOn; }
            set { _routine.PerformedOn = value; }
        }

        public DateTime MinDate { get { return new DateTime(2000,1,1);} }

        public DateTime MaxDate { get { return DateTime.Today; } }

        public MvvmCommand Save
        {
            get
            {
                return new MvvmCommand(o =>
                {                    
                    _exerciseService.AddEditRoutine(_routine);
                    _navigationManager.Return();                    
                });
            }
        }

        public MvvmCommand Cancel
        {
            get
            {
                return new MvvmCommand(o =>
                {
                    _navigationManager.Return();
                });
            }
        }
    }
}
