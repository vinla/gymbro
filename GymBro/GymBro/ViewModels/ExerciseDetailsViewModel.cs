using System;

namespace GymBro.ViewModels
{
    public class ExerciseDetailsViewModel : Core.ViewModel
    {
        private readonly Data.ExerciseService _exerciseService;
        private readonly Core.NavigationManager _navigationManager;
        private readonly Models.Exercise _exercise;

        public ExerciseDetailsViewModel(Core.NavigationManager navigationManager, Data.ExerciseService exerciseService, Models.Exercise exercise)
        {
            _exerciseService = exerciseService;
            _navigationManager = navigationManager;
            _exercise = exercise;
        }

        public String PageTitle
        {
            get { return _exercise.Name; }
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
                    Weight = "25kg"
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
                    Weight = "Na"
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
    }
}
