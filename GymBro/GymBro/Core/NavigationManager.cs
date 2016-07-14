using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GymBro.Core
{
    public class NavigationManager
    {
        private readonly Dictionary<Type, Type> _lookup;
        private NavigationPage _navigationPage;

        public NavigationManager()
        {
            _lookup = new Dictionary<Type, Type>();
        }

        public void RegisterView<TViewModelType, TViewType>()
        {
            // TOOD: Check that the view model type implements ViewModel
            var viewType = typeof(TViewType);
            var viewModelType = typeof(TViewModelType);
            _lookup.Add(viewModelType, viewType);
        }

        public NavigationPage GetRoot(object rootViewModel)
        {
            var rootPage = CreatePage(rootViewModel);
            _navigationPage = new NavigationPage(rootPage);            
            return _navigationPage;
        }

        public async void Push(object viewModel)
        {
            var page = CreatePage(viewModel);                     
            await _navigationPage.PushAsync(page);            
        }

        private Page CreatePage(object viewModel)
        {
            var viewModelType = viewModel.GetType();
            var viewType = _lookup[viewModelType];

            var page = Activator.CreateInstance(viewType) as Page;
            page.BindingContext = viewModel;
            NavigationPage.SetHasNavigationBar(page, false);
            return page;
        }

        public async void Return()
        {
            await _navigationPage.Navigation.PopAsync();
        }

        public async void DisplayAlert(String alertTitle, String alertText, String buttonText)
        {
            await _navigationPage.DisplayAlert(alertTitle, alertText, buttonText);
        }

        public async Task<Boolean> DisplayAlert(String alertTitle, String alertText, String acceptText, String cancelText)
        {
            return await _navigationPage.DisplayAlert(alertTitle, alertText, acceptText, cancelText);
        }
    }    
}
