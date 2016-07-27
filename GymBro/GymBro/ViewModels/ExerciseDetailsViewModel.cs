using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GymBro.Core;
using GymBro.Models;

namespace GymBro.ViewModels
{
    public class ExerciseDetailsViewModel : ViewModel
    {
        private readonly Data.ExerciseService _exerciseService;
        private readonly NavigationManager _navigationManager;
        private readonly List<Person> _people;
        private Models.Exercise _exercise;
        private Boolean _optionsVisible;
        private int _currentPerson;
        
        public ExerciseDetailsViewModel(NavigationManager navigationManager, Data.ExerciseService exerciseService, Models.Exercise exercise)
        {
            _exerciseService = exerciseService;
            _navigationManager = navigationManager;
            _exercise = exercise;
            _people = _exerciseService.GetPersons().ToList();
            _currentPerson = -1;
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

        public MvvmCommand AddEntry
        {
            get
            {
                return new MvvmCommand(o =>
                {
                    if (_currentPerson > -1)
                    {
                        var viewModel = new AddEntryViewModel(_navigationManager, _exerciseService, _exercise, CurrentPerson);
                        _navigationManager.Push(viewModel);
                    }
                });
            }
        }

        public Person CurrentPerson
        {
            get
            {
                if (_currentPerson == -1)
                    return new Person {Id = -1, DisplayName = "Everyone"};
                else
                {
                    return _people[_currentPerson];
                }
            }
        }

        public MvvmCommand NextPerson
        {
            get
            {
                return new MvvmCommand(o =>
                {
                    _currentPerson++;
                    if (_currentPerson >= _people.Count)
                        _currentPerson = -1;
                    RaisePropertyChanged("CurrentPerson");
                    RaisePropertyChanged("ExerciseData");
                });
            }
        }

        public MvvmCommand PreviousPerson
        {
            get
            {
                return new MvvmCommand(o =>
                {
                    _currentPerson--;
                    if (_currentPerson < -1)
                        _currentPerson = _people.Count - 1;
                    RaisePropertyChanged("CurrentPerson");
                    RaisePropertyChanged("ExerciseData");
                });
            }
        }       

        public IEnumerable<IGrouping<String, ExerciseInfo>> ExerciseData
        {
            get
            {
                var data = _exerciseService.GetRoutines(_exercise.Id);
                if (_currentPerson > -1)
                    data = data.Where(ex => ex.Person.Id == CurrentPerson.Id).ToList();

                return data.Select(ex => new ExerciseInfo
                {
                    Date = ex.PerformedOn.ToString("dd-MMM-yyyy"),
                    BackColor = ex.Person.ColorCode,
                    Initials = ex.Person.Initials,
                    Info = String.Format("Sets: {0} | Reps: {1} | Weight: {2}", ex.NumberOfSets, ex.NumberOfReps, ex.WeightInKilos)
                })
                .GroupBy(ex => ex.Date);
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

    public class ExerciseInfo
    {
        public String Date { get; set; }
        public String BackColor { get; set; }
        public String Initials { get; set; }
        public String Info { get; set; }
    }

    public class Grouping<TKey, TData> : ObservableCollection<TData>
    {
        public TKey Key { get; private set; }

        public Grouping(TKey key, IEnumerable<TData> items)
        {
            Key = key;
            foreach (var item in items)
                this.Items.Add(item);
        }
    }
}
