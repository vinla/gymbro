using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace GymBro.Views
{
    public partial class ExerciseList : ContentPage
    {
        public ExerciseList()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var viewModel = BindingContext as Core.ViewModel;
            if (viewModel != null)
                viewModel.OnActivating();            
        }
    }
}
