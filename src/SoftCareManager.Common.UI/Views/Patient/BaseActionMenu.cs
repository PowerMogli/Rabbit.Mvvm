using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using SoftCareManager.Contracts.Groups.Items;
using SoftCareManager.Contracts.Groups.Selection;
using SoftCareManager.Contracts.Model;

namespace SoftCareManager.Common.UI.Views.Patient
{
    public class BaseActionMenu<TModel> : 
        UserControl, 
        ISelectionSubscriber<TModel>, 
        IItemsSubscriber
        where TModel : IModel
    {
        public TModel SelectedItem
        {
            get { return (TModel)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(TModel), typeof(BaseActionMenu<TModel>),
                new PropertyMetadata(null, OnSelectedItemPropertyChanged));

        private static void OnSelectedItemPropertyChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            var dataContext = dependencyObject.GetValue(DataContextProperty) as ISelectionSubscriber;
            if (dataContext == null)
            {
                return;
            }

            dataContext.SelectedItem = e.NewValue;
        }

        public ObservableCollection<object> Items
        {
            get { return (ObservableCollection<object>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<object>), typeof(BaseActionMenu<TModel>),
                new PropertyMetadata(null, OnItemsPropertyChanged));

        private static void OnItemsPropertyChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            var dataContext = dependencyObject.GetValue(DataContextProperty) as IItemsSubscriber;
            if (dataContext == null)
            {
                return;
            }

            dataContext.Items = e.NewValue as ObservableCollection<object>;
        }

        #region ISelectionSubscriber
        object ISelectionSubscriber.SelectedItem
        {
            get { return SelectedItem; }
            set { SelectedItem = (TModel)value; }
        }

        #endregion
    }
}