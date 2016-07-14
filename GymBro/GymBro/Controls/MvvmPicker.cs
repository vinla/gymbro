using System;
using Xamarin.Forms;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace GymBro.Controls
{
    public class MvvmPicker : Picker
    {
        public MvvmPicker()
        {
            this.SelectedIndexChanged += OnSelectedIndexChanged;            
        }

        public readonly static BindableProperty ItemsSourceProperty = BindableProperty.CreateAttached("ItemSource",
            typeof (IEnumerable), typeof (MvvmPicker), default(IEnumerable), propertyChanged: OnItemsSourceChanged);

        public readonly static BindableProperty SelectedItemProperty = BindableProperty.CreateAttached("SelectedItem",
            typeof (object), typeof (MvvmPicker), default(object), propertyChanged: OnSelectedItemChanged);

        public readonly static BindableProperty DisplayPropertyProperty = BindableProperty.CreateAttached("DisplayProperty",
            typeof (String), typeof (MvvmPicker), default(IEnumerable), propertyChanged: OnDisplayPropertyChanged);
            
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public String DisplayProperty
        {
            get { return (String) GetValue(DisplayPropertyProperty); }
            set { SetValue(DisplayPropertyProperty, value);}
        }

        private static void OnDisplayPropertyChanged(BindableObject bindable, Object oldValue, Object newValue)
        {
            var picker = bindable as MvvmPicker;
            if (picker == null)
                throw new InvalidOperationException();
            picker.RefreshItemList();
        }

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var picker = bindable as MvvmPicker;

            if(picker == null)
                throw new InvalidOperationException();

            var newItems = newValue as IEnumerable;

            if(newItems == null)
                throw new InvalidOperationException();
            
            picker.RefreshItemList();
        }

        private void RefreshItemList()
        {            
            Items.Clear();

            foreach (var item in ItemsSource)
            {                
                Items.Add(GetItemDisplay(item));
            }
        }

        private String GetItemDisplay(Object item)
        {
            if (String.IsNullOrEmpty(DisplayProperty))
                return item.ToString();
            return item.GetType().GetRuntimeProperty(DisplayProperty).GetValue(item)?.ToString();
        }

        private void OnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            if (SelectedIndex < 0 || SelectedIndex > Items.Count - 1)
            {
                SelectedItem = null;
            }
            else
            {
                SelectedItem = ItemsSource.Cast<Object>().ToList()[SelectedIndex];
            }
        }

        private static void OnSelectedItemChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
           
        }
    }
}