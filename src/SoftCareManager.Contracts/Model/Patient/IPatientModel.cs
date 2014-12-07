using System;
using System.Collections.ObjectModel;

using SoftCareManager.Contracts.Model.Therapy;

namespace SoftCareManager.Contracts.Model.Patient
{
    public interface IPatientModel : IModel
    {
        DateTime Birthday { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        ObservableCollection<ITherapyModel> Therapies { get; set; }
    }
}