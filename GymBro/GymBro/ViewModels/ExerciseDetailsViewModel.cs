using System;
using GymBro.Core;

namespace GymBro.ViewModels
{
    public class ExerciseDetailsViewModel : Core.ViewModel
    {
        private readonly Data.ExerciseService _exerciseService;
        private readonly Core.NavigationManager _navigationManager;
        private readonly Models.Exercise _exercise;
        private Boolean _optionsVisible;

        public ExerciseDetailsViewModel(Core.NavigationManager navigationManager, Data.ExerciseService exerciseService, Models.Exercise exercise)
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

        public MvvmCommand DrillDown
        {
            get
            {
                return new MvvmCommand(o =>
                {
                    var detailViewModel = new RoutineDataViewModel(_navigationManager, _exerciseService, _exercise);
                    _navigationManager.Push(detailViewModel);
                });
            }
        }

        public RoutineViewModel R1
        {
            get
            {
                return new RoutineViewModel
                {
                    PersonName = "Vincent",
                    DatePerformed = "04 July 2016",
                    Reps = "12",
                    Sets = "4",
                    Weight = "25kg",
                    DrillDown = DrillDown
                };
            }
        }

        public RoutineViewModel R2
        {
            get
            {
                return new RoutineViewModel
                {
                    PersonName = "Kim",
                    DatePerformed = "Never",
                    Reps = "Na",
                    Sets = "Na",
                    Weight = "Na",
                    DrillDown = DrillDown
                };
            }
        }
    }

    public class RoutineViewModel
    {
        public String PersonName { get; set; }
        public String DatePerformed { get; set; }
        public String Reps { get; set; }
        public String Sets { get; set; }
        public String Weight { get; set; }
        public MvvmCommand DrillDown { get; set; }
    }
}
