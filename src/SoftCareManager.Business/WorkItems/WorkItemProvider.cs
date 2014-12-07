using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

using SoftCareManager.Contracts.WorkItems;

namespace SoftCareManager.Business.WorkItems
{
    [Export(typeof (IWorkItemProvider))]
    public class WorkItemProvider : IWorkItemProvider
    {
        [ImportMany]
        private IEnumerable<Lazy<IWorkItem, IWorkItemMetaData>> WorkItems { get; set; }

        public TWorkItem Get<TWorkItem>() where TWorkItem : IWorkItem
        {
            Lazy<IWorkItem, IWorkItemMetaData> lazyWorkItem = (from workItem in WorkItems
                                                               where workItem.Metadata.WorkItemName.Equals(typeof (TWorkItem).Name)
                                                               select workItem).FirstOrDefault();

            return lazyWorkItem != null
                ? (TWorkItem)lazyWorkItem.Value
                : default(TWorkItem);
        }

        public object Get(Type workItemType)
        {
            Lazy<IWorkItem, IWorkItemMetaData> lazyWorkItem = (from workItem in WorkItems
                                                               where workItem.Metadata.WorkItemType == workItemType
                                                               select workItem).FirstOrDefault();

            return lazyWorkItem != null
                ? lazyWorkItem.Value
                : null;
        }
    }
}