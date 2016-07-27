using System;
using System.Collections.Generic;
using System.Linq;
using GymBro.Core;
using GymBro.Models;

namespace GymBro.ViewModels
{
    public class ExerciseViewModel
    {
        private readonly Data.ExerciseService _exerciseService;
        private readonly Exercise _exercise;
        private readonly NavigationManager _navigationManager;
        private readonly List<ExerciseCategory> _categories;

        public ExerciseViewModel(Data.ExerciseService exerciseService, NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            _exerciseService = exerciseService;
            _exercise = new Exercise {Id = -1, Category = ExerciseCategory.Misc };
            _categories = EnumerationExtensions.EnumToList<ExerciseCategory>().Skip(1).ToList();
        }

        public ExerciseViewModel(Data.ExerciseService exerciseService, NavigationManager navigationManager, Exercise exercise)
        {
            _navigationManager = navigationManager;
            _exerciseService = exerciseService;
            _exercise = new Exercise
            {
                Id = exercise.Id,
                Name = exercise.Name,
                Category = exercise.Category
            };
            _categories = EnumerationExtensions.EnumToList<ExerciseCategory>().Skip(1).ToList();
        }

        public String PageTitle => _exercise.Id > -1 ? "Edit exercise" : "New exercise";

        public String Name
        {
            get { return _exercise.Name; }
            set { _exercise.Name = value; }
        }

        public List<ExerciseCategory> Categories => _categories;

        public ExerciseCategory SelectedCategory
        {
            get { return _exercise.Category; }
            set { _exercise.Category = value; }
        }
        
        public MvvmCommand Save
        {
            get
            {
                return new MvvmCommand(o =>
                {
                    if (_exercise.Id == -1)
                    {                        
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
