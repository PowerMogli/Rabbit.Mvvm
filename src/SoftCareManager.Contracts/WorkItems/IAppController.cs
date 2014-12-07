using System;

using SoftCareManager.Contracts.Services;
using SoftCareManager.Contracts.ViewModel;

namespace SoftCareManager.Contracts.WorkItems
{
    public interface IAppController
    {
        event EventHandler InitializationCompleted;

        ViewModelBase BuildViewModel(Type viewModelType);

        void Execute();

        TService GetService<TService>()
            where TService : IService;

        object GetService(Type serviceType);

        TWorkItem GetWorkItem<TWorkItem>()
            where TWorkItem : IWorkItem;

        object GetWorkItem(Type workItemType);

        void RegisterOnEventBroker(object item);
    }
}