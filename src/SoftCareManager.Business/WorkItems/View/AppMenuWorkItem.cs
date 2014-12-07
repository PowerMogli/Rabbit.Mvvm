using System.ComponentModel.Composition;

using SoftCareManager.Contracts.General;
using SoftCareManager.Contracts.Model;
using SoftCareManager.Contracts.WorkItems;

namespace SoftCareManager.Business.WorkItems.View
{
    [Export(typeof (IWorkItem))]
    [Workflow(WorkItemName.AppMenuWorkItem, WorkItemName = WorkItemName.AppMenuWorkItem)]
    public class AppMenuWorkItem : WorkItem<IAppMenuModel>
    {
        [ImportingConstructor]
        public AppMenuWorkItem(IAppController appWorkItem)
        {
            RootWorkItem = appWorkItem;
        }
    }
}