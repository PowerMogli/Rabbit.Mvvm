using SoftCareManager.Contracts.Services;
using System;

namespace SoftCareManager.Contracts.WorkItems
{
    public interface IAppController
    {
        TService GetService<TService>()
            where TService : IService;

        TWorkItem GetWorkItem<TWorkItem>()
            where TWorkItem : IWorkItem;

        object GetWorkItem(Type workItemType);

        object GetService(Type serviceType);

        void RegisterOnEventBroker(object item);

        void Execute();

        event EventHandler InitializationCompleted;
    }
}