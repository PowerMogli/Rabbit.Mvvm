using System.ComponentModel.Composition;

using SoftCareManager.Contracts.General;
using SoftCareManager.Contracts.Model.Patient;
using SoftCareManager.Contracts.WorkItems;
using SoftCareManager.Contracts.WorkItems.Patient;

namespace SoftCareManager.Business.WorkItems.Patient
{
    [Export(typeof (IWorkItem))]
    [Workflow(WorkItemName.PatientInsuranceWorkItem, WorkItemName = WorkItemName.PatientInsuranceWorkItem, WorkItemType = typeof (IPatientInsuranceWorkItem))]
    public class PatientInsuranceWorkItem : WorkItem<IPatientModel>, IPatientInsuranceWorkItem
    {
        [ImportingConstructor]
        public PatientInsuranceWorkItem(IAppController appWorkItem)
        {
            RootWorkItem = appWorkItem;
        }
    }
}