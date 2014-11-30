using SoftCareManager.Business.WorkItems.Patient;
using SoftCareManager.Contracts.Groups.Items;
using SoftCareManager.Contracts.Groups.Selection;
using SoftCareManager.Contracts.Model.Patient;
using SoftCareManager.Contracts.ViewModel;
using SoftCareManager.Contracts.WorkItems;
using SoftCareManager.Contracts.WorkItems.Patient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SoftCareManager.ViewModel.Patient
{
    public class PatientListOverviewViewModel : ViewModelBase, IPatientOverviewModel, ISelectionPublisher, IItemsPublisher
    {
        private IAppController _appController;
        private IPatientOverviewWorkItem patientOverviewWorkItem;

        private object _selectedItem;
        public object SelectedItem
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
            get
            {
                return _patients;
            }
            private set
            {
                _patients = value;
                RaisePropertyChanged();
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
            patientOverviewWorkItem = appController.GetWorkItem<IPatientOverviewWorkItem>();
        }

        public override void Initialize(Contracts.Application.Navigation.INavigationParameter navigationParameter)
        {
            base.Initialize(navigationParameter);

            LoadPatients();
        }

        private async void LoadPatients()
        {
            await Task.Delay(2000);
            await Task.Run(() =>
            {
                var patientViewModels = MapModel(patientOverviewWorkItem.GetPatients("naip:Berlin"));
                Patients = new ObservableCollection<IPatientModel>(patientViewModels);
            });
        }

        private IEnumerable<IPatientModel> MapModel(IEnumerable<IPatientModel> patientModels)
        {
            List<PatientViewModel> patientViewModels = new List<PatientViewModel>();

            foreach (var patientModel in patientModels)
            {
                var patientViewModel = new PatientViewModel(new PatientArticleWorkItem(_appController), new PatientHospitalWorkItem(_appController), new PatientInsuranceWorkItem(_appController))
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

        public IEnumerable<object> Items
        {
            get { return _patients; }
            set
            {
                //_patients = value;
                RaisePropertyChanged();
            }
        }
    }
}