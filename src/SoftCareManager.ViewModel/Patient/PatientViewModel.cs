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
        private DateTime _birthday;
        private string _firstName;
        private string _lastName;
        private readonly IPatientWorkItemFactory _patientWorkItemFactory;

        public PatientViewModel(IPatientWorkItemFactory patientWorkItemFactory)
        {
            _patientWorkItemFactory = patientWorkItemFactory;

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
            Therapies = _patientWorkItemFactory.TherapyWorkItem.LoadTherapies(Id);
        }
    }
}