using SoftCareManager.Contracts.Groups.Items;
using SoftCareManager.Contracts.Groups.Selection;
using SoftCareManager.Contracts.Model.Patient;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SoftCareManager.Common.UI.Views.Patient
{
    public class BasePatientActionMenu : UserControl, ISelectionSubscriber, IItemsSubscriber
    {
        public IPatientModel SelectedItem
        {
            get { return (IPatientModel)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(IPatientModel), typeof(BasePatientActionMenu), new PropertyMetadata(null, OnSelectedItemPropertyChanged));

        private static void OnSelectedItemPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var dataContext = dependencyObject.GetValue(FrameworkElement.DataContextProperty) as ISelectionSubscriber;
            if (dataContext == null)
            {
                return;
            }

            dataContext.SelectedItem = e.NewValue as IPatientModel;
        }

        public ObservableCollection<IPatientModel> Items
        {
            get { return (ObservableCollection<IPatientModel>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ObservableCollection<IPatientModel>), typeof(BasePatientActionMenu), new PropertyMetadata(null, OnItemsPropertyChanged));

        private static void OnItemsPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var dataContext = dependencyObject.GetValue(FrameworkElement.DataContextProperty) as IItemsSubscriber;
            if (dataContext == null)
            {
                return;
            }

            dataContext.Items = e.NewValue as ObservableCollection<IPatientModel>;
        }
    }
}