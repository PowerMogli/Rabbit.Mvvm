using SoftCareManager.Contracts;
using SoftCareManager.Contracts.Application.Navigation;
using SoftCareManager.Contracts.Application.Region;
using SoftCareManager.Contracts.General;
using SoftCareManager.Contracts.Services;
using SoftCareManager.Contracts.ViewModel;
using SoftCareManager.Contracts.WorkItems;
using System;
using System.ComponentModel.Composition;
using System.Linq;

namespace SoftCareManager.Business.Services.Application
{
    [ServiceAttribute(ServiceName = Service.NavigationProxy)]
    public class NavigationProxy : INavigationProxy
    {
        private IObjectBuilder objectBuilder;
        private INavigationService navigationService;
        private IRegionManager regionManager;
        private IAppController appWorkItem;

        [ImportingConstructor]
        public NavigationProxy(INavigationService navigationService, IObjectBuilder objectBuilder, IRegionManager regionManager, IAppController appWorkItem)
        {
            this.objectBuilder = objectBuilder;
            this.navigationService = navigationService;
            this.regionManager = regionManager;
            this.appWorkItem = appWorkItem;
        }

        public void RequestNavigation(INavigationParameter navigationParameter, IAppController appWorkItem)
        {
            RequestNavigation(navigationParameter, nr => { }, appWorkItem);
        }

        public void RequestNavigation(INavigationParameter navigationParameter, Action<INavigationResult> navigationCallback, IAppController appController)
        {
            if (RequestMultiNavigation(navigationParameter, navigationCallback, appController))
            {
                return;
            }

            ViewModelBase viewModel = TryGetHostContent(navigationParameter);

            if (IsNavigationAllowed(viewModel) == false)
            {
                return;
            }

            if (MustBuildInstance(viewModel, navigationParameter.ViewModelType))
            {
                viewModel = objectBuilder.Build(navigationParameter.ViewModelType, appController) as ViewModelBase;
            }

            viewModel.Initialize(navigationParameter);

            navigationService.RequestNavigation(GetRegion(navigationParameter), viewModel, navigationCallback);
        }

        private bool MustBuildInstance(ViewModelBase viewModel, Type viewModelType)
        {
            return viewModel == null || viewModel.GetType() != viewModelType;
        }

        private IRegion GetRegion(INavigationParameter navigationParameter)
        {
            return regionManager.Regions.FirstOrDefault(
                r => (r.Name == navigationParameter.RegionName && navigationParameter.ShellId == r.ShellId)
                || (r.ShellId == -1 && r.Name == navigationParameter.RegionName));
        }

        private ViewModelBase TryGetHostContent(INavigationParameter navigationParameter)
        {
            var region = GetRegion(navigationParameter);
            if (region == null)
            {
                return null;
            }

            return region.GetContent(navigationParameter.ViewModelType);
        }

        private bool RequestMultiNavigation(INavigationParameter navigationParameter, Action<INavigationResult> navigationCallback, IAppController appController)
        {
            var mergedNavigationParameter = navigationParameter as IMergedNavigationParameter;
            if (mergedNavigationParameter == null)
            {
                return false;
            }

            foreach (var naviParameter in mergedNavigationParameter.NavigationParameter)
            {
                RequestNavigation(naviParameter, navigationCallback, appController);
            }

            return true;
        }

        private bool IsNavigationAllowed(ViewModelBase viewModel)
        {
            var navigationAware = viewModel as INavigationAware;

            return navigationAware == null || navigationAware.NavigateFrom();
        }
    }
}