using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

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
        public static readonly DependencyProperty ItemsProperty = ItemsControl.ItemsSourceProperty.AddOwner(typeof(BaseActionMenu<TModel>),
                                                                                                            new PropertyMetadata(null, OnItemsPropertyChanged));

        public static readonly DependencyProperty SelectedItemProperty = Selector.SelectedItemProperty.AddOwner(typeof(BaseActionMenu<TModel>),
                                                                                                                new PropertyMetadata(null, OnSelectedItemPropertyChanged));

        public ObservableCollection<object> Items
        {
            get { return (ObservableCollection<object>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public TModel SelectedItem
        {
            get { return (TModel)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        private static void OnItemsPropertyChanged(DependencyObject dependencyObject,
                                                   DependencyPropertyChangedEventArgs e)
        {
            IItemsSubscriber dataContext = dependencyObject.GetValue(DataContextProperty) as IItemsSubscriber;
            if (dataContext == null)
            {
                return;
            }

            dataContext.Items = e.NewValue as ObservableCollection<object>;
        }

        private static void OnSelectedItemPropertyChanged(DependencyObject dependencyObject,
                                                          DependencyPropertyChangedEventArgs e)
        {
            ISelectionSubscriber dataContext = dependencyObject.GetValue(DataContextProperty) as ISelectionSubscriber;
            if (dataContext == null)
            {
                return;
            }

            dataContext.SelectedItem = e.NewValue;
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