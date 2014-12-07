using System;
using System.Collections.ObjectModel;

using SoftCareManager.Contracts.Model.Therapy;

namespace SoftCareManager.Contracts.WorkItems.Patient
{
    public interface IPatientTherapyWorkItem
    {
        ObservableCollection<ITherapyModel> LoadTherapies(Guid? patientId);
    }
}