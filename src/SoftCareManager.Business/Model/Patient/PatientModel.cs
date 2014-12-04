using SoftCareManager.Contracts.Model.Patient;
using SoftCareManager.Contracts.Model.Therapy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SoftCareManager.Business.Model.Patient
{
    public class PatientModel : IPatientModel
    {
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public DateTime Birthday { get; set; }

        public Guid? Id { get; set; }

        public ObservableCollection<ITherapyModel> Therapies { get; set; }
    }
}