using System;
using System.Collections;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace GymBro.Controls
{
    // in lieu of an actual Xamarin Forms ItemsControl, this is a heavily modified version of code from https://forums.xamarin.com/discussion/21635/xforms-needs-an-itemscontrol
    public class RepeaterView : StackLayout
    {
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create<RepeaterView, IEnumerable>(
            p => p.ItemsSource,
            null,
            BindingMode.OneWay,
            propertyChanged: ItemsChanged);

        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create<RepeaterView, DataTemplate>(
            p => p.ItemTemplate,
            default(DataTemplate));

        public static readonly BindableProperty ItemTemplateSelectorProperty =
            BindableProperty.CreateAttached("ItemTemplateSelector", typeof(DataTemplateSelector), typeof(RepeaterView), null);

        public event RepeaterViewItemAddedEventHandler ItemCreated;

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public DataTemplateSelector ItemTemplateSelector
        {
            get { return GetValue(ItemTemplateSelectorProperty) as DataTemplateSelector; }
            set { SetValue(ItemTemplateSelectorProperty, value); }
        }

        private static void ItemsChanged(BindableObject bindable, IEnumerable oldValue, IEnumerable newValue)
        {
            var control = (RepeaterView)bindable;
            var oldObservableCollection = oldValue as INotifyCollectionChanged;

            if (oldObservableCollection != null)
            {
                oldObservableCollection.CollectionChanged -= control.OnItemsSourceCollectionChanged;
            }

            var newObservableCollection = newValue as INotifyCollectionChanged;

            if (newObservableCollection != null)
            {
                newObservableCollection.CollectionChanged += control.OnItemsSourceCollectionChanged;
            }

            control.Children.Clear();

            if (newValue != null)
            {
                foreach (var item in newValue)
                {
                    var view = control.CreateChildViewFor(item);
                    control.Children.Add(view);
                    control.OnItemCreated(view);
                }
            }

            control.UpdateChildrenLayout();
            control.InvalidateLayout();
        }

        protected virtual void OnItemCreated(View view) =>
            this.ItemCreated?.Invoke(this, new RepeaterViewItemAddedEventArgs(view, view.BindingContext));

        private void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var invalidate = false;

            if (e.OldItems != null)
            {
                this.Children.RemoveAt(e.OldStartingIndex);
                invalidate = true;
            }

            if (e.NewItems != null)
            {
                for (var i = 0; i < e.NewItems.Count; ++i)
                {
                    var item = e.NewItems[i];
                    var view = this.CreateChildViewFor(item);

                    this.Children.Insert(i + e.NewStartingIndex, view);
                    OnItemCreated(view);
                }

                invalidate = true;
            }

            if (invalidate)
            {
                this.UpdateChildrenLayout();
                this.InvalidateLayout();
            }
        }

        private View CreateChildViewFor(object item)
        {
            var template = ItemTemplate;

            if (ItemTemplateSelector != null)
            {
                template = ItemTemplateSelector.SelectTemplate(item, this);
            }

            template.SetValue(BindableObject.BindingContextProperty, item);
            return (View)template.CreateContent();
        }
    }

    public class RepeaterViewItemAddedEventArgs : EventArgs
    {
        private readonly View view;
        private readonly object model;

        public RepeaterViewItemAddedEventArgs(View view, object model)
        {
            this.view = view;
            this.model = model;
        }

        public View View => this.view;

        public object Model => this.model;
    }

    public delegate void RepeaterViewItemAddedEventHandler(object sender, RepeaterViewItemAddedEventArgs args);

}
