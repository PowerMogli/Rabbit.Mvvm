using System;
using System.Collections.ObjectModel;

using SoftCareManager.Contracts.Model.Patient;
using SoftCareManager.Contracts.Model.Therapy;

namespace SoftCareManager.Business.Model.Patient
{
    public class PatientModel : IPatientModel
    {
        public DateTime Birthday { get; set; }

        public string FirstName { get; set; }

        public Guid? Id { get; set; }

        public string LastName { get; set; }

        public ObservableCollection<ITherapyModel> Therapies { get; set; }
    }
}