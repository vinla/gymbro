using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace GymBro.Controls
{
    public class MvvmListView : ListView
    {
        public MvvmListView()
        {
            ItemSelected += (sender, e) => {
                ((ListView)sender).SelectedItem = null;
            };

            ItemTapped += (sender, e) =>
            {                
                ItemSelectedCommand?.Execute(e.Item);
            };
        }

        public static BindableProperty ItemSelectedCommandProperty = BindableProperty.Create("ItemSelectedCommand", typeof(ICommand), typeof(MvvmListView));

        public ICommand ItemSelectedCommand
        {
            get { return GetValue(ItemSelectedCommandProperty) as ICommand; }
            set { SetValue(ItemSelectedCommandProperty, value); }
        }
    }
}
