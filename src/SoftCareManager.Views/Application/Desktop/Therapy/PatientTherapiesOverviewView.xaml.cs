using SoftCareManager.Common.UI.Groups.Items;
using SoftCareManager.Common.UI.Groups.Selection;
using SoftCareManager.Contracts.General;
using SoftCareManager.Contracts.Groups.Items;
using SoftCareManager.Contracts.Groups.Selection;
using System.Windows;

namespace SoftCareManager.Views.Application.Desktop.Therapy
{
    public partial class PatientTherapiesOverviewView
    {
        public PatientTherapiesOverviewView()
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
                SelectionGroupManager.SetSelectionGroupSource(this, new SelectionGroupSource(Groups.PatientTherapySelectionGroupName, DataContext as ISelectionPublisher));
                ItemsGroupManager.SetItemsGroupSource(this, new ItemsGroupSource(Groups.PatientTherapyItemsGroupName, DataContext as IItemsPublisher));
            };

            return loadedEventHandler;
        }
    }
}