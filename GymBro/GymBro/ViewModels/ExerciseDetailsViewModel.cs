using System;
using System.Collections.Generic;
using System.Linq;
using GymBro.Core;

namespace GymBro.ViewModels
{
    public class ExerciseDetailsViewModel : ViewModel
    {
        private readonly Data.ExerciseService _exerciseService;
        private readonly NavigationManager _navigationManager;
        private Models.Exercise _exercise;
        private Boolean _optionsVisible;

        public ExerciseDetailsViewModel(NavigationManager navigationManager, Data.ExerciseService exerciseService, Models.Exercise exercise)
        {
            _exerciseService = exerciseService;
            _navigationManager = navigationManager;
            _exercise = exercise;
        }

        public String ExerciseName
        {
            get { return _exercise.Name; }
        }

        public Boolean OptionsVisible
        {
            get {  return _optionsVisible; }
            set { _optionsVisible = value; RaisePropertyChanged("OptionsVisible"); }
        }

        public MvvmCommand ToggleOptions
        {
            get
            {
                return new MvvmCommand(o =>
                {
                    OptionsVisible = !OptionsVisible;
                });
            }
        }

        public MvvmCommand Edit
        {
            get
            {
                return new MvvmCommand(o =>
                {
                    var editViewModel = new ViewModels.ExerciseViewModel(_exerciseService, _navigationManager, _exercise);                    
                    _navigationManager.Push(editViewModel);
                });
            }
        }

        public MvvmCommand Delete
        {
            get
            {
                return new MvvmCommand(async o =>
                {
                    if (
                        await
                            _navigationManager.DisplayAlert("Confirm delete",
                                "Are you sure you want to delete this exercise. All related data will be deleted and this action cannot be undone",
                                "Delete", "Keep"))
                    {
                        _exerciseService.DeleteExercise(_exercise.Id);
                        _navigationManager.Return();
                    }
                });
            }
        }

        public MvvmCommand DrillDown
        {
            get
            {
                return new MvvmCommand(o =>
                {
                    var person = o as Models.Person;

                    if(person == null)
                        throw new InvalidOperationException();

                    var detailViewModel = new RoutineDataViewModel(_navigationManager, _exerciseService, _exercise, person);
                    _navigationManager.Push(detailViewModel);
                });
            }
        }

        public List<RoutineViewModel> PersonData
        {
            get
            {
                var routineViewModels = new List<RoutineViewModel>();
                var persons = _exerciseService.GetPersons();
                foreach (var person in persons)
                {
                    var latestRoutine = _exerciseService.GetRoutines(_exercise.Id, person.Id).OrderByDescending(r => r.PerformedOn).FirstOrDefault();
                    if (latestRoutine != null)
                    {
                        routineViewModels.Add(new RoutineViewModel
                        {
                            Person = person,
                            DatePerformed = latestRoutine.PerformedOn.ToString("dd-MMMM-yyyy"),
                            Reps = latestRoutine.NumberOfReps.ToString(),
                            Sets = latestRoutine.NumberOfSets.ToString(),
                            Weight = latestRoutine.WeightInKilos.ToString(),
                            Colour = person.ColorCode
                        });
                    }
                    else
                    {
                        routineViewModels.Add(new RoutineViewModel
                        {
                            Person = person,
                            DatePerformed = "No records",
                            Reps = "N/A",
                            Sets = "N/A",
                            Weight = "N/A",
                            Colour = person.ColorCode
                        });
                    }
                }

                return routineViewModels;
            }
        }

        public override void OnActivating()
        {
            OptionsVisible = false;
            RaisePropertyChanged("PersonData");
            _exercise = _exerciseService.GetExercises().Single(ex => ex.Id == _exercise.Id);
            RaisePropertyChanged("ExerciseName");
        }
    }

    public class RoutineViewModel
    {
        public Models.Person Person { get; set; }
        public String DatePerformed { get; set; }
        public String Reps { get; set; }
        public String Sets { get; set; }
        public String Weight { get; set; }
        public String Colour { get; set; }
    }
}
