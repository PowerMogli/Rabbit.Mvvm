using System;

namespace SoftCareManager.Contracts.WorkItems
{
    public interface IWorkItemMetaData
    {
        string WorkItemName { get; }

        Type WorkItemType { get; }
    }
}