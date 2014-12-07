using System;
using System.Collections.ObjectModel;

using SoftCareManager.Contracts.Model.Patient;
using SoftCareManager.Contracts.Model.Therapy;
using SoftCareManager.Contracts.ViewModel;
using SoftCareManager.Contracts.WorkItems.Patient;

namespace SoftCareManager.ViewModel.Patient
{
    public class PatientViewModel : ViewModelBase, IPatientModel
    {
        private readonly IPatientTherapyWorkItem _patientTherapyWorkItem;
        private DateTime _birthday;
        private string _firstName;
        private string _lastName;
        private IPatientArticleWorkItem _patientArticleWorkItem;
        private IPatientHospitalWorkItem _patientHospitalWorkItem;
        private IPatientInsuranceWorkItem _patientInsuranceWorkItem;

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

        public DateTime Birthday
        {
            get { return _birthday; }
            set
            {
                _birthday = value;
                RaisePropertyChanged();
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                RaisePropertyChanged();
            }
        }

        public Guid? Id { get; set; }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ITherapyModel> Therapies { get; set; }

        private void LoadTherapies()
        {
            Therapies = _patientTherapyWorkItem.LoadTherapies(Id);
        }
    }
}