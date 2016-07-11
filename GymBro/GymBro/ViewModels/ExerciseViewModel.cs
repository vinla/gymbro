using System;

namespace GymBro.ViewModels
{
    public class ExerciseViewModel
    {
        private readonly Data.ExerciseService _exerciseService;
        private readonly Core.NavigationManager _navigationManager;

        public ExerciseViewModel(Data.ExerciseService exerciseService, Core.NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            _exerciseService = exerciseService;
        }
        
        public String Name { get; set; }

        public Core.MvvmCommand Save
        {
            get
            {
                return new Core.MvvmCommand(o =>
                {
                    var newExercise = new Models.Exercise { Id = -1, Name = Name };
                    _exerciseService.AddExercise(newExercise);
                    _navigationManager.Return();
                });
            }
        }

        public Core.MvvmCommand Cancel
        {
            get
            {
                return new Core.MvvmCommand(o =>
                {
                    _navigationManager.Return();
                });
            }
        }
    }
}
