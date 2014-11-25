using System;

namespace SoftCareManager.Contracts.WorkItems
{
    public interface IWorkItemProvider
    {
        TWorkItem Get<TWorkItem>() where TWorkItem : IWorkItem;

        object Get(Type workItemType);
    }
}