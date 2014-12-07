using System.ComponentModel.Composition;
using System.Windows;

using SoftCareManager.Common.UI.Authorization;
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
    [Export(typeof (AppShellContainerView))]
    public partial class AppShellContainerView : IPartImportsSatisfiedNotification
    {
        private readonly AppShellContainerViewModel _dataContext;
        private readonly IBaseGroupManager<ItemsGroup, ItemsGroupSource> _itemsGroupManager;
        private readonly IRegionManager _regionManager;
        private readonly IBaseGroupManager<SelectionGroup, SelectionGroupSource> _selectionGroupManager;

        [ImportingConstructor]
        public AppShellContainerView(IRegionManager regionManager, IBaseGroupManager<SelectionGroup, SelectionGroupSource> selectionGroupManager, IBaseGroupManager<ItemsGroup, ItemsGroupSource> itemsGroupManager, IAppController appController)
        {
            InitializeComponent();

            _dataContext = new AppShellContainerViewModel(appController, new AppShellSwitch());
            DataContext = _dataContext;

            _regionManager = regionManager;
            _itemsGroupManager = itemsGroupManager;
            _selectionGroupManager = selectionGroupManager;

            Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = SkinService.DesktopUri
            });
        }

        public void OnImportsSatisfied()
        {
            RegionManager.SetRegionManager(this, _regionManager);
            SelectionGroupManager.SetSelectionGroupManager(this, _selectionGroupManager);
            ItemsGroupManager.SetItemsGroupManager(this, _itemsGroupManager);

            InitializeVisibilityBinder();

            _regionManager.Update();
        }

        private void InitializeVisibilityBinder()
        {
            UIVisibilityBinder visibilityBinder = new UIVisibilityBinder();
            UIVisibilityBinder.SetUiVisibilityBinder(this, visibilityBinder);
            _dataContext.VisibilityBinder = visibilityBinder;
        }
    }
}