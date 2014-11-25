using SoftCareManager.Contracts.Model.Patient;
using SoftCareManager.Contracts.ViewModel;
using SoftCareManager.Contracts.WorkItems;
using SoftCareManager.Contracts.WorkItems.Patient;
using System;

namespace SoftCareManager.ViewModel.Patient
{
    public class PatientViewModel : ViewModelBase, IPatientModel
    {
        private IPatientArticleWorkItem patientArticleWorkItem;
        private IPatientHospitalWorkItem patientHospitalWorkItem;
        private IPatientInsuranceWorkItem patientInsuranceWorkItem;

        public Guid? Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public DateTime Birthday { get; set; }

        public PatientViewModel(IPatientArticleWorkItem patientArticleWorkItem, IPatientHospitalWorkItem patientHospitalWorkItem, IPatientInsuranceWorkItem patientInsuranceWorkItem)
        {
            this.patientArticleWorkItem = patientArticleWorkItem;
            this.patientHospitalWorkItem = patientHospitalWorkItem;
            this.patientInsuranceWorkItem = patientInsuranceWorkItem;
        }
    }
}