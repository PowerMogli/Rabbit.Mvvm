using SoftCareManager.Contracts.General;
using SoftCareManager.Contracts.Model;
using System.ComponentModel.Composition;

namespace SoftCareManager.Contracts.WorkItems.View
{
    [Export(typeof(IWorkItem))]
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