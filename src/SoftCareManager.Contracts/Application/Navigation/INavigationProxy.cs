using SoftCareManager.Contracts.Services;
using SoftCareManager.Contracts.WorkItems;
using System;

namespace SoftCareManager.Contracts.Application.Navigation
{
    public interface INavigationProxy : IService
    {
        void RequestNavigation(INavigationParameter navigationParameter, IAppController appController);

        void RequestNavigation(INavigationParameter navigationParameter, Action<INavigationResult> navigationCallback, IAppController appController);
    }
}