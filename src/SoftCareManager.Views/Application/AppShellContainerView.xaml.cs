using System.ComponentModel.Composition;
using System.Windows;
using SoftCareManager.Common.UI.Groups.Base;
using SoftCareManager.Common.UI.Groups.Items;
using SoftCareManager.Common.UI.Groups.Selection;
using SoftCareManager.Common.UI.Region;
using SoftCareManager.Common.UI.Services;
using SoftCareManager.Contracts.Application.Region;
using SoftCareManager.Contracts.WorkItems;
using SoftCareManager.ViewModel.Application;

namespace SoftCareManager.Views.Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Export(typeof(AppShellContainerView))]
    public partial class AppShellContainerView : IPartImportsSatisfiedNotification
    {
        private readonly IRegionManager _regionManager;
        private readonly IBaseGroupManager<SelectionGroup, SelectionGroupSource> _selectionGroupManager;
        private readonly IBaseGroupManager<ItemsGroup, ItemsGroupSource> _itemsGroupManager;

        [ImportingConstructor]
        public AppShellContainerView(IRegionManager regionManager, IBaseGroupManager<SelectionGroup, SelectionGroupSource> selectionGroupManager, IBaseGroupManager<ItemsGroup, ItemsGroupSource> itemsGroupManager, IAppController appController)
        {
            InitializeComponent();

            DataContext = new AppShellContainerViewModel(appController, new AppShellSwitch());

            _regionManager = regionManager;
            _itemsGroupManager = itemsGroupManager;
            _selectionGroupManager = selectionGroupManager;

            Resources.MergedDictionaries.Add(new ResourceDictionary { Source = SkinService.DesktopUri });
        }

        public void OnImportsSatisfied()
        {
            RegionManager.SetRegionManager(this, _regionManager);
            SelectionGroupManager.SetSelectionGroupManager(this, _selectionGroupManager);
            ItemsGroupManager.SetItemsGroupManager(this, _itemsGroupManager);

            _regionManager.Update();
        }
    }
}