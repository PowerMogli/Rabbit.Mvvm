using SoftCareManager.Common.UI.Region;
using SoftCareManager.Common.UI.Services;
using SoftCareManager.Contracts.Application.Region;
using SoftCareManager.Contracts.WorkItems;
using SoftCareManager.ViewModel.Application;
using System.ComponentModel.Composition;
using System.Windows;

namespace SoftCareManager.Views.Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Export(typeof(AppShellContainerView))]
    public partial class AppShellContainerView : IPartImportsSatisfiedNotification
    {
        private readonly IRegionManager _regionManager;

        [ImportingConstructor]
        public AppShellContainerView(IRegionManager regionManager, IAppController appController)
        {
            InitializeComponent();

            DataContext = new AppShellContainerViewModel(appController, new AppShellSwitch());

            _regionManager = regionManager;

            Resources.MergedDictionaries.Add(new ResourceDictionary { Source = SkinService.DesktopUri });
        }

        public void OnImportsSatisfied()
        {
            RegionManager.SetRegionManager(this, _regionManager);

            _regionManager.Update();
        }
    }
}