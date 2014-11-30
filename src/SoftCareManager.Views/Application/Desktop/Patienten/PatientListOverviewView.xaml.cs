using SoftCareManager.Common.UI.Groups.Items;
using SoftCareManager.Common.UI.Groups.Selection;
using SoftCareManager.Contracts.General;
using SoftCareManager.Contracts.Groups.Items;
using SoftCareManager.Contracts.Groups.Selection;
using System.Windows;

namespace SoftCareManager.Views.Application.Desktop
{
    /// <summary>
    /// Interaction logic for PatientOverviewView.xaml
    /// </summary>
    public partial class PatientListOverviewView
    {
        public PatientListOverviewView()
        {
            InitializeComponent();
            Loaded += OnLoaded();
        }

        private RoutedEventHandler OnLoaded()
        {
            RoutedEventHandler loadedEventHandler = null;
            loadedEventHandler = (s, e) =>
            {
                Loaded -= loadedEventHandler;
                SelectionGroupManager.SetSelectionGroupSource(this, new SelectionGroupSource(Groups.PatientListSelectionGroupName, this.DataContext as ISelectionPublisher));
                ItemsGroupManager.SetItemsGroupSource(this, new ItemsGroupSource(Groups.PatientListItemsGroupName, this.DataContext as IItemsPublisher));
            };

            return loadedEventHandler;
        }
    }
}