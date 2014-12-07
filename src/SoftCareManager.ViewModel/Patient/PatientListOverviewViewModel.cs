using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using SoftCareManager.Business.WorkItems.Patient;
using SoftCareManager.Contracts.Application.Navigation;
using SoftCareManager.Contracts.Groups.Items;
using SoftCareManager.Contracts.Groups.Selection;
using SoftCareManager.Contracts.Model.Patient;
using SoftCareManager.Contracts.ViewModel;
using SoftCareManager.Contracts.WorkItems;
using SoftCareManager.Contracts.WorkItems.Patient;

namespace SoftCareManager.ViewModel.Patient
{
    public class PatientListOverviewViewModel : ViewModelBase, IPatientOverviewModel, ISelectionPublisher<IPatientModel>, IItemsPublisher
    {
        private readonly IAppController _appController;
        private readonly IPatientOverviewWorkItem _patientOverviewWorkItem;
        private bool _loadedPatients;

        private IPatientModel _selectedItem;

        public IPatientModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<IPatientModel> _patients;

        public ObservableCollection<IPatientModel> Patients
        {
            get { return _patients; }
            private set
            {
                _patients = value;
                RaisePropertyChanged();
                Items = Patients.Cast();
            }
        }

        public Guid? Id { get; set; }

#if TEST
        public PatientOverviewViewModel(IWorkItem1 workItem1, IWorkItem2 workItem2, ... etc.)
        {
            this.WorkItem1 = workItem1;
            this.WorkItem2 = workItem2;
            ....
        }
#endif

        public PatientListOverviewViewModel(IAppController appController)
        {
            _appController = appController;
            CanBeActivated = true;
            _patientOverviewWorkItem = appController.GetWorkItem<IPatientOverviewWorkItem>();
        }

        public override void Initialize(INavigationParameter navigationParameter)
        {
            base.Initialize(navigationParameter);

            if (_loadedPatients == false)
            {
                LoadPatients();
            }
        }

        private async void LoadPatients()
        {
            await Task.Delay(2000);
            await Task.Run(() =>
            {
                IEnumerable<IPatientModel> patientViewModels = MapModel(_patientOverviewWorkItem.GetPatients("naip:Berlin"));
                Patients = new ObservableCollection<IPatientModel>(patientViewModels);
                _loadedPatients = true;
            });
        }

        private IEnumerable<IPatientModel> MapModel(IEnumerable<IPatientModel> patientModels)
        {
            List<PatientViewModel> patientViewModels = new List<PatientViewModel>();

            foreach (var patientModel in patientModels)
            {
                PatientViewModel patientViewModel = new PatientViewModel(
                    new PatientArticleWorkItem(_appController),
                    new PatientHospitalWorkItem(_appController),
                    new PatientInsuranceWorkItem(_appController),
                    new PatientTherapyWorkItem())
                {
                    Id = patientModel.Id,
                    LastName = patientModel.LastName,
                    FirstName = patientModel.FirstName,
                    Birthday = patientModel.Birthday
                };

                patientViewModels.Add(patientViewModel);
            }

            return patientViewModels;
        }

        private ObservableCollection<object> _items;

        public ObservableCollection<object> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged();
            }
        }
    }
}