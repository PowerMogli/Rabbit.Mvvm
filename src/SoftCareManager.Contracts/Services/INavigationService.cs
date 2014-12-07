using System;

using SoftCareManager.Contracts.Application.Navigation;
using SoftCareManager.Contracts.Application.Region;
using SoftCareManager.Contracts.ViewModel;

namespace SoftCareManager.Contracts.Services
{
    public interface INavigationService : IService
    {
        void RequestNavigation(IRegion region, ViewModelBase viewModel, Action<INavigationResult> navigationCallback);
    }
}