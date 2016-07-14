using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace GymBro
{
    public partial class App : Application
    {
        private readonly Core.NavigationManager _navigationManager;

        public App()
        {
            InitializeComponent();

            _navigationManager = new Core.NavigationManager();
            _navigationManager.RegisterView<ViewModels.ExerciseListViewModel, Views.ExerciseList>();
            _navigationManager.RegisterView<ViewModels.ExerciseViewModel, Views.Exercise>();
            _navigationManager.RegisterView<ViewModels.ExerciseDetailsViewModel, Views.ExerciseDetailsPage>();
            _navigationManager.RegisterView<ViewModels.RoutineDataViewModel, Views.RoutineDataView>();

            var dataConnection = DependencyService.Get<Data.IConnectionProvider>().GetConnection();
            var exerciseService = new Data.ExerciseService(dataConnection);
            var mainViewModel = new ViewModels.ExerciseListViewModel(exerciseService, _navigationManager);

            MainPage = _navigationManager.GetRoot(mainViewModel);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
