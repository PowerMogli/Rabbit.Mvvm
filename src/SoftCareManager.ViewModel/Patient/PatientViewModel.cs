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

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                RaisePropertyChanged();
            }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value; RaisePropertyChanged();
            }
        }

        private DateTime _birthday;
        public DateTime Birthday
        {
            get { return _birthday; }
            set
            {
                _birthday = value; RaisePropertyChanged();
            }
        }

        public PatientViewModel(IPatientArticleWorkItem patientArticleWorkItem, IPatientHospitalWorkItem patientHospitalWorkItem, IPatientInsuranceWorkItem patientInsuranceWorkItem)
        {
            this.patientArticleWorkItem = patientArticleWorkItem;
            this.patientHospitalWorkItem = patientHospitalWorkItem;
            this.patientInsuranceWorkItem = patientInsuranceWorkItem;
        }
    }
}