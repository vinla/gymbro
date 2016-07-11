using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace GymBro.ViewModels
{
    public class ExerciseListViewModel : Core.ViewModel
    {
        private readonly Data.ExerciseService _exerciseService;
        private readonly Core.NavigationManager _navigationManager;
        private List<Models.Exercise> _exercises;
        private Models.ExerciseCategory _currentFilter;

        public ExerciseListViewModel(Data.ExerciseService exerciseService, Core.NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            _exerciseService = exerciseService;            
        }

        public IEnumerable<Models.Exercise> Exercises
        {
            get
            {
                if(_currentFilter == Models.ExerciseCategory.All)
                    return _exercises;
                else
                {
                    return _exercises.Where(ex => ex.Category == _currentFilter);
                }
            }
        }

        public Core.MvvmCommand NextFilter
        {
            get
            {
                return new Core.MvvmCommand(o =>
                {
                    Int32 value = (Int32)_currentFilter + 1;
                    if (value > 8)
                        value = 0;
                    _currentFilter = (Models.ExerciseCategory)value;
                    RaisePropertyChanged("Exercises");
                    RaisePropertyChanged("CurrentFilterName");
                });
            }
        }

        public Core.MvvmCommand PreviousFilter
        {
            get
            {
                return new Core.MvvmCommand(o =>
               {
                   Int32 value = (Int32)_currentFilter - 1;
                   if (value < 0)
                       value = 8;
                   _currentFilter = (Models.ExerciseCategory)value;
                   RaisePropertyChanged("Exercises");
                   RaisePropertyChanged("CurrentFilterName");
               });
            }
        }

        public String CurrentFilterName
        {
            get { return _currentFilter.ToString(); }
        }

        public Core.MvvmCommand ShowDetails
        {
            get
            {
                return new Core.MvvmCommand(o =>
                {
                    var exercise = o as Models.Exercise;
                    var viewModel = new ExerciseDetailsViewModel(_navigationManager, _exerciseService, exercise);
                    _navigationManager.Push(viewModel);
                });
            }
        }

        public ICommand AddExercise
        {
            get
            {
                return new Core.MvvmCommand(o =>
                {
                    var viewModel = new ExerciseViewModel(_exerciseService, _navigationManager);
                    _navigationManager.Push(viewModel);
                });
            }
        }       

        public override void OnActivating()
        {
            _exercises = _exerciseService.GetExercises().ToList();
            RaisePropertyChanged("Exercises");
        }
    }
}
