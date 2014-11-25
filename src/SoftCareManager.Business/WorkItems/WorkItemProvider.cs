using SoftCareManager.Contracts.WorkItems;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace SoftCareManager.Business.Services
{
    [Export(typeof(IWorkItemProvider))]
    public class WorkItemProvider : IWorkItemProvider
    {
        [ImportMany]
        IEnumerable<Lazy<IWorkItem, IWorkItemMetaData>> WorkItems { get; set; }

        public TWorkItem Get<TWorkItem>() where TWorkItem : IWorkItem
        {
            var lazyWorkItem = (from workItem in WorkItems
                                where workItem.Metadata.WorkItemName.Equals(typeof(TWorkItem).Name)
                                select workItem).FirstOrDefault();

            return (TWorkItem)lazyWorkItem.Value;
        }

        public object Get(Type workItemType)
        {
            var lazyWorkItem = (from workItem in WorkItems
                                where workItem.Metadata.WorkItemType.Equals(workItemType)
                                select workItem).FirstOrDefault();

            return lazyWorkItem.Value;
        }
    }
}