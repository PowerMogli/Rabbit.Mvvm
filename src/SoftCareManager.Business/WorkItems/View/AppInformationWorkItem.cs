using System.ComponentModel.Composition;

using SoftCareManager.Contracts.General;
using SoftCareManager.Contracts.Model;
using SoftCareManager.Contracts.WorkItems;
using SoftCareManager.Contracts.WorkItems.View;

namespace SoftCareManager.Business.WorkItems.View
{
    [Export(typeof (IWorkItem))]
    [Workflow(WorkItemName.AppInformationWorkItem, WorkItemName = WorkItemName.AppInformationWorkItem, WorkItemType = typeof (IAppInformationWorkItem))]
    public class AppInformationWorkItem : WorkItem<IAppInformationModel>, IAppInformationWorkItem
    {
    }
}