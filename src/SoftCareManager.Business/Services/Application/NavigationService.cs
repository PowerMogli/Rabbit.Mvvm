using SoftCareManager.Contracts.Application.Navigation;
using SoftCareManager.Contracts.Application.Region;
using SoftCareManager.Contracts.Services;
using SoftCareManager.Contracts.ViewModel;
using System;
using System.ComponentModel.Composition;

namespace SoftCareManager.Business.Services.Application
{
    [Export(typeof(INavigationService))]
    public class NavigationService : INavigationService
    {
        public void RequestNavigation(IRegion region, ViewModelBase viewModel, Action<INavigationResult> navigationCallback)
        {
            try
            {
                region.Activate(viewModel);

                InvokeOnNavigationAwareElement(viewModel);
            }
            catch (Exception exception)
            {
                NotifyNavigationFailed(navigationCallback, exception);
            }
        }

        private void InvokeOnNavigationAwareElement(ViewModelBase viewModel)
        {
            var navigationAware = viewModel as INavigationAware;

            if (navigationAware == null)
            {
                return;
            }

            navigationAware.NavigateTo();
        }

        private void NotifyNavigationFailed(Action<NavigationResult> navigationCallback, Exception e)
        {
            var navigationResult =
                e != null ? new NavigationResult(true, new NavigationException(e.Message, e)) : new NavigationResult(false);

            navigationCallback(navigationResult);
        }
    }
}