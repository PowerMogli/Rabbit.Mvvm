using SoftCareManager.Contracts.General;
using SoftCareManager.Contracts.Model.Patient;
using SoftCareManager.Contracts.WorkItems;
using SoftCareManager.Contracts.WorkItems.Patient;
using System.ComponentModel.Composition;

namespace SoftCareManager.Business.WorkItems.Patient
{
    [Export(typeof(IWorkItem))]
    [Workflow(WorkItemName.PatientHospitalWorkItem, WorkItemName = WorkItemName.PatientHospitalWorkItem, WorkItemType = typeof(IPatientHospitalWorkItem))]
    public class PatientHospitalWorkItem : WorkItem<IPatientModel>, IPatientHospitalWorkItem
    {
        [ImportingConstructor]
        public PatientHospitalWorkItem(IAppController appWorkItem)
        {
            this.RootWorkItem = appWorkItem;
        }
    }
}