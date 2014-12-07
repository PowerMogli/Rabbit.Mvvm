using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;

using SoftCareManager.Business.Model.Therapy;
using SoftCareManager.Contracts.General;
using SoftCareManager.Contracts.Model.Patient;
using SoftCareManager.Contracts.Model.Therapy;
using SoftCareManager.Contracts.WorkItems;
using SoftCareManager.Contracts.WorkItems.Patient;

namespace SoftCareManager.Business.WorkItems.Patient
{
    [Export(typeof (IWorkItem))]
    [Workflow(WorkItemName.PatientTherapyWorkItem, WorkItemName = WorkItemName.PatientTherapyWorkItem, WorkItemType = typeof (IPatientTherapyWorkItem))]
    public class PatientTherapyWorkItem : WorkItem<IPatientModel>, IPatientTherapyWorkItem
    {
        public ObservableCollection<ITherapyModel> LoadTherapies(Guid? patientId)
        {
            return new ObservableCollection<ITherapyModel>
            {
                new TherapyModel
                {
                    Name = "Stoma",
                    Anmerkung = "Das tut weh!"
                },
                new TherapyModel
                {
                    Name = "Parenterale Ernährung",
                    Anmerkung = "Hab großen Hunger"
                },
                new TherapyModel
                {
                    Name = "Wundversorgung",
                    Anmerkung = "Großes Aua am Zeh!"
                }
            };
        }
    }
}