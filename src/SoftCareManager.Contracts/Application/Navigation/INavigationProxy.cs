using System;

using SoftCareManager.Contracts.Services;
using SoftCareManager.Contracts.WorkItems;

namespace SoftCareManager.Contracts.Application.Navigation
{
    public interface INavigationProxy : IService
    {
        void RequestNavigation(INavigationParameter navigationParameter, IAppController appController);

        void RequestNavigation(INavigationParameter navigationParameter, Action<INavigationResult> navigationCallback, IAppController appController);
    }
}