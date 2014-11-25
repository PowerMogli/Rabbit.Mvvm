using SoftCareManager.Business.WorkItems.Patient;
using SoftCareManager.Contracts.Model.Patient;
using SoftCareManager.Contracts.ViewModel;
using SoftCareManager.Contracts.WorkItems;
using SoftCareManager.Contracts.WorkItems.Patient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SoftCareManager.ViewModel.Patient
{
    public class PatientOverviewViewModel : ViewModelBase, IPatientOverviewModel
    {
        private IAppController appWorkItem;
        private IPatientOverviewWorkItem patientOverviewWorkItem;
        private ObservableCollection<IPatientModel> patients;

        public ObservableCollection<IPatientModel> Patients
        {
            get
            {
                return patients;
            }
            private set
            {
                patients = value;
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
        public PatientOverviewViewModel(IAppController appWorkItem)
        {
            this.appWorkItem = appWorkItem;
            CanBeActivated = true;
            patientOverviewWorkItem = appWorkItem.GetWorkItem<IPatientOverviewWorkItem>();
            LoadPatients();
        }

        private void LoadPatients()
        {
            var patientViewModels = MapModel(patientOverviewWorkItem.GetPatients("naip:Berlin"));
            Patients = new ObservableCollection<IPatientModel>(patientViewModels);
        }

        private IEnumerable<IPatientModel> MapModel(IEnumerable<IPatientModel> patientModels)
        {
            List<PatientViewModel> patientViewModels = new List<PatientViewModel>();

            foreach (var patientModel in patientModels)
            {
                var patientViewModel = new PatientViewModel(new PatientArticleWorkItem(appWorkItem), new PatientHospitalWorkItem(appWorkItem), new PatientInsuranceWorkItem(appWorkItem))
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
    }
}