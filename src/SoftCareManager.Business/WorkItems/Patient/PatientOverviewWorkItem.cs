using SoftCareManager.Contracts.General;
using SoftCareManager.Contracts.Model;
using SoftCareManager.Contracts.Model.Patient;
using SoftCareManager.Contracts.Services;
using SoftCareManager.Contracts.WorkItems;
using SoftCareManager.Contracts.WorkItems.Patient;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace SoftCareManager.Business.WorkItems.Patient
{
    [Export(typeof(IWorkItem))]
    [Workflow(WorkItemName.PatientOverviewWorkItem, WorkItemName = WorkItemName.PatientOverviewWorkItem, WorkItemType = typeof(IPatientOverviewWorkItem))]
    public class PatientOverviewWorkItem : IPatientOverviewWorkItem
    {
        public IModel Model { get; set; }

        public IAppController RootWorkItem { get; set; }

        public IEnumerable<IPatientModel> GetPatients(string network)
        {
            var patientProvider = RootWorkItem.GetService<IPatientProvider>();

            return patientProvider.GetPatients(network);
        }
    }
}