using SoftCareManager.Contracts.Model.Therapy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SoftCareManager.Contracts.WorkItems.Patient
{
    public interface IPatientTherapyWorkItem
    {
        ObservableCollection<ITherapyModel> LoadTherapies(Guid? patientId);
    }
}