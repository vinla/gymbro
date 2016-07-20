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
        private readonly Exercise _exercise;
        private readonly Person _person;

        public AddEntryViewModel(NavigationManager navigationManager, ExerciseService exerciseService, Exercise exercise,
            Person person)
        {
            _navigationManager = navigationManager;
            _exerciseService = exerciseService;
            _exercise = exercise;
            _person = person;
            NumberOfReps = 12;
            NumberOfSets = 4;
            WeightInKilos = 10;
            PerformedOn = DateTime.Today;
        }

        public Exercise Exercise => _exercise;

        public Person Person => _person;

        public Int32 NumberOfReps { get; set; }

        public Int32 NumberOfSets { get; set; }

        public Single WeightInKilos { get; set; }

        public DateTime PerformedOn { get; set; }

        public DateTime MinDate { get { return new DateTime(2000,1,1);} }

        public DateTime MaxDate { get { return DateTime.Today; } }

        public MvvmCommand Save
        {
            get
            {
                return new MvvmCommand(o =>
                {
                    var newRoutine = new Routine
                    {
                        ExerciseId = _exercise.Id,                                                
                        PersonId = _person.Id,
                        NumberOfReps = NumberOfReps,
                        NumberOfSets = NumberOfSets,
                        PerformedOn = PerformedOn,
                        WeightInKilos = WeightInKilos
                    };
                    _exerciseService.AddRoutine(newRoutine);
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
