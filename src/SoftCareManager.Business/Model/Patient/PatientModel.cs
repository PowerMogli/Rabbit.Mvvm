using SoftCareManager.Contracts.Model.Patient;
using System;

namespace SoftCareManager.Business.Model.Patient
{
    public class PatientModel : IPatientModel
    {
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public DateTime Birthday { get; set; }

        public Guid? Id { get; set; }
    }
}