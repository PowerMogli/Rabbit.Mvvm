using SoftCareManager.Common;
using SoftCareManager.Common.UI.Region;
using SoftCareManager.Common.UI.Service;
using SoftCareManager.Contracts.Application.Navigation;
using SoftCareManager.Contracts.Application.Region;
using SoftCareManager.Contracts.General;
using SoftCareManager.Contracts.ViewModel;
using SoftCareManager.ViewModel.Application;
using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;

namespace SoftCareManager.Views.Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Export(typeof(AppShellContainerView))]
    public partial class AppShellContainerView : IPartImportsSatisfiedNotification
    {
        private IRegionManager regionManager;

        [ImportingConstructor]
        public AppShellContainerView(IRegionManager regionManager,
            [Import(ViewModelName.DesktopMainViewModel, typeof(ViewModelBase))]ViewModelBase dataContext)
        {
            InitializeComponent();

            this.DataContext = dataContext;

            this.regionManager = regionManager;

            Loaded += OnLoaded();

            Resources.MergedDictionaries.Add(new ResourceDictionary { Source = SkinService.DesktopUri });
        }

        private RoutedEventHandler OnLoaded()
        {
            RoutedEventHandler loadedEventHandler = null;
            loadedEventHandler = (s, arg) =>
            {
                Loaded -= loadedEventHandler;

                NavigateTo(new NavigationParameter(Regions.AppMenuView, typeof(AppMenuViewModel)));
                NavigateTo(new NavigationParameter(Regions.AppShellMenuView, typeof(AppShellMenuViewModel)));

                LoadedCommand.Execute(new Action(() =>
                {
                    ShellMonitor.SetShellMonitor(this, true);
                    regionManager.Update();
                }));
            };

            return loadedEventHandler;
        }

        public void OnImportsSatisfied()
        {
            RegionManager.SetRegionManager(this, regionManager);

            regionManager.Update();
        }

        private void NavigateTo(INavigationParameter navigationParameter)
        {
            navigationParameter.SetShellId(ShellId);
            NavigateToCommand.Execute(navigationParameter);
        }

        private void CanExecuteGeneral(object sender, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = Shell != null;
            args.Handled = true;
        }

        private void ExecuteShellChange(object sender, ExecutedRoutedEventArgs args)
        {
            ShellId = Convert.ToInt32(args.Parameter);
        }

        private void ExecuteUIPlatformChange(object sender, ExecutedRoutedEventArgs args)
        {
            IsTouch = !IsTouch;

            ChangeSkin.Execute(null);

            var navigationParameter = new NavigationParameter(Regions.AppMenuView, typeof(AppMenuViewModel));
            navigationParameter.SetStayVisible(true);

            NavigateTo(navigationParameter);

            navigationParameter = new NavigationParameter(Regions.AppInformationView, typeof(AppInformationViewModel));
            navigationParameter.SetStayVisible(true);

            NavigateTo(navigationParameter);
        }

        private void ExecuteNavigateTo(object sender, ExecutedRoutedEventArgs e)
        {
            NavigateTo(e.Parameter as INavigationParameter);
        }
    }
}