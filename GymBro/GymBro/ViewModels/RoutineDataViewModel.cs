using System;
using System.Collections.Generic;
using System.Linq;
using GymBro.Core;
using GymBro.Models;

namespace GymBro.ViewModels
{
    public class RoutineDataViewModel : ViewModel
    {
        private readonly Data.ExerciseService _exerciseService;
        private readonly NavigationManager _navigationManager;
        private readonly Exercise _exercise;
        private readonly Person _person;

        public RoutineDataViewModel(Core.NavigationManager navigationManager, Data.ExerciseService exerciseService, Models.Exercise exercise, Models.Person person)
        {
            _navigationManager = navigationManager;
            _exerciseService = exerciseService;
            _exercise = exercise;
            _person = person;            
        }

        public String PersonName => _person.DisplayName;

        public String ExerciseName => _exercise.Name;

        public List<Routine> Data
        {
            get
            {
                return
                    _exerciseService.GetRoutines(_exercise.Id, _person.Id)
                        .OrderByDescending(r => r.PerformedOn)
                        .ToList();
            }
        }

        public MvvmCommand AddEntry
        {
            get
            {
                return new MvvmCommand(o =>
                {
                    var viewModel = new AddEntryViewModel(_navigationManager, _exerciseService, _exercise, _person);
                    _navigationManager.Push(viewModel);
                });
            }
        }

        public override void OnActivating()
        {
            RaisePropertyChanged("Data");
        }
    }
}
