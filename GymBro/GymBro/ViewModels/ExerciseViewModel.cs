using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using GymBro.Core;
using GymBro.Models;

namespace GymBro.ViewModels
{
    public class ExerciseViewModel
    {
        private readonly Data.ExerciseService _exerciseService;
        private readonly Models.Exercise _exercise;
        private readonly Core.NavigationManager _navigationManager;
        private readonly List<Models.ExerciseCategory> _categories;

        public ExerciseViewModel(Data.ExerciseService exerciseService, Core.NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            _exerciseService = exerciseService;
            _exercise = new Exercise {Id = -1, Category = ExerciseCategory.Misc };
            _categories = EnumerationExtensions.EnumToList<Models.ExerciseCategory>().Skip(1).ToList();
        }

        public ExerciseViewModel(Data.ExerciseService exerciseService, Core.NavigationManager navigationManager, Models.Exercise exercise)
        {
            _navigationManager = navigationManager;
            _exerciseService = exerciseService;
            _exercise = new Exercise
            {
                Id = exercise.Id,
                Name = exercise.Name,
                Category = exercise.Category
            };
            _categories = EnumerationExtensions.EnumToList<Models.ExerciseCategory>().Skip(1).ToList();
        }

        public String Name
        {
            get { return _exercise.Name; }
            set { _exercise.Name = value; }
        }

        public List<Models.ExerciseCategory> Categories { get { return _categories;} }

        public Models.ExerciseCategory SelectedCategory
        {
            get { return _exercise.Category; }
            set { _exercise.Category = value; }
        }
        
        public Core.MvvmCommand Save
        {
            get
            {
                return new Core.MvvmCommand(o =>
                {
                    if (_exercise.Id == -1)
                    {
                        _exercise.Id = _exerciseService.GetExercises().Max(ex => ex.Id) + 1;
                        _exerciseService.AddExercise(_exercise);
                    }
                    else
                    {
                        _exerciseService.UpdateExercise(_exercise.Id, _exercise.Name, _exercise.Category);
                    }
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
