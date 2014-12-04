using SoftCareManager.Contracts.Model.Therapy;
using System;
using System.Collections.ObjectModel;

namespace SoftCareManager.Contracts.Model.Patient
{
    public interface IPatientModel : IModel
    {
        string LastName { get; set; }

        string FirstName { get; set; }

        DateTime Birthday { get; set; }

        ObservableCollection<ITherapyModel> Therapies { get; set; }
    }
}