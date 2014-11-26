using System;
using System.ComponentModel.Composition;
using System.Linq;
using SoftCareManager.Contracts;
using SoftCareManager.Contracts.Application.Navigation;
using SoftCareManager.Contracts.Application.Region;
using SoftCareManager.Contracts.General;
using SoftCareManager.Contracts.Services;
using SoftCareManager.Contracts.ViewModel;
using SoftCareManager.Contracts.WorkItems;

namespace SoftCareManager.Business.Services.Application
{
    [Service(ServiceName = Service.NavigationProxy)]
    public class NavigationProxy : INavigationProxy
    {
        private readonly IObjectBuilder _objectBuilder;
        private readonly INavigationService _navigationService;
        private readonly IRegionManager _regionManager;
        private readonly IAppController _appWorkItem;

        [ImportingConstructor]
        public NavigationProxy(INavigationService navigationService, IObjectBuilder objectBuilder,
            IRegionManager regionManager, IAppController appWorkItem)
        {
            _objectBuilder = objectBuilder;
            _navigationService = navigationService;
            _regionManager = regionManager;
            _appWorkItem = appWorkItem;
        }

        public void RequestNavigation(INavigationParameter navigationParameter)
        {
            RequestNavigation(navigationParameter, nr => { });
        }

        public void RequestNavigation(INavigationParameter navigationParameter,
            Action<INavigationResult> navigationCallback)
        {
            if (RequestMultiNavigation(navigationParameter, navigationCallback))
            {
                return;
            }

            var viewModel = TryGetHostContent(navigationParameter);

            if (IsNavigationAllowed(viewModel) == false)
            {
                return;
            }

            viewModel = BuildViewModel(navigationParameter, viewModel);

            if (viewModel == null)
            {
                return;
            }

            viewModel.Initialize(navigationParameter);

            _navigationService.RequestNavigation(GetRegion(navigationParameter), viewModel, navigationCallback);
        }

        private ViewModelBase BuildViewModel(INavigationParameter navigationParameter, ViewModelBase viewModel)
        {
            if (MustBuildInstance(viewModel, navigationParameter.ViewModelType) == false)
            {
                return viewModel;
            }

            return _objectBuilder.Build(navigationParameter.ViewModelType, _appWorkItem) as ViewModelBase;
        }

        private static bool MustBuildInstance(ViewModelBase viewModel, Type viewModelType)
        {
            return viewModel == null || viewModel.GetType() != viewModelType;
        }

        private IRegion GetRegion(INavigationParameter navigationParameter)
        {
            return _regionManager.Regions.FirstOrDefault(
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

        private bool RequestMultiNavigation(INavigationParameter navigationParameter,
            Action<INavigationResult> navigationCallback)
        {
            var mergedNavigationParameter = navigationParameter as IMergedNavigationParameter;
            if (mergedNavigationParameter == null)
            {
                return false;
            }

            foreach (var naviParameter in mergedNavigationParameter.NavigationParameter)
            {
                RequestNavigation(naviParameter, navigationCallback);
            }

            return true;
        }

        private static bool IsNavigationAllowed(ViewModelBase viewModel)
        {
            var navigationAware = viewModel as INavigationAware;

            return navigationAware == null || navigationAware.NavigateFrom();
        }
    }
}
