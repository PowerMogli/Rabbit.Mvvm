using SoftCareManager.Contracts.Model.Patient;
using SoftCareManager.Contracts.Model.Therapy;
using SoftCareManager.Contracts.ViewModel;
using SoftCareManager.Contracts.WorkItems.Patient;
using System;
using System.Collections.ObjectModel;

namespace SoftCareManager.ViewModel.Patient
{
    public class PatientViewModel : ViewModelBase, IPatientModel
    {
        private IPatientArticleWorkItem _patientArticleWorkItem;
        private IPatientHospitalWorkItem _patientHospitalWorkItem;
        private IPatientInsuranceWorkItem _patientInsuranceWorkItem;
        private readonly IPatientTherapyWorkItem _patientTherapyWorkItem;

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

        public PatientViewModel(IPatientArticleWorkItem patientArticleWorkItem,
            IPatientHospitalWorkItem patientHospitalWorkItem,
            IPatientInsuranceWorkItem patientInsuranceWorkItem,
            IPatientTherapyWorkItem patientTherapyWorkItem)
        {
            _patientArticleWorkItem = patientArticleWorkItem;
            _patientHospitalWorkItem = patientHospitalWorkItem;
            _patientInsuranceWorkItem = patientInsuranceWorkItem;
            _patientTherapyWorkItem = patientTherapyWorkItem;

            LoadTherapies();
        }

        private void LoadTherapies()
        {
            Therapies = _patientTherapyWorkItem.LoadTherapies(Id);
        }

        public ObservableCollection<ITherapyModel> Therapies { get; set; }
    }
}