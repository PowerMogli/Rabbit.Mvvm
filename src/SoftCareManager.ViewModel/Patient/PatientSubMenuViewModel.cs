using System;
using System.Collections.Generic;

using SoftCareManager.Business.WorkItems.Patient;
using SoftCareManager.Common;
using SoftCareManager.Contracts.Application.Navigation;
using SoftCareManager.Contracts.General;
using SoftCareManager.Contracts.ViewModel;
using SoftCareManager.ViewModel.Therapy;

namespace SoftCareManager.ViewModel.Patient
{
    public class PatientSubMenuViewModel : ViewModelBase, INavigationAware
    {
        private INavigationParameter _patientTherapyParameter;
        private INavigationParameter patientStammdatenParameter;

        public INavigationParameter PatientDokumentationParameter { get; set; }

        public INavigationParameter PatientStammdatenParameter
        {
            get { return patientStammdatenParameter; }
            set
            {
                patientStammdatenParameter = value;
                RaisePropertyChanged();
            }
        }

        public INavigationParameter PatientTherapyParameter
        {
            get { return _patientTherapyParameter; }
            set
            {
                _patientTherapyParameter = value;
                RaisePropertyChanged();
            }
        }

        public bool NavigateFrom()
        {
            return true;
        }

        public void NavigateTo()
        {
        }

        protected override void InnerInitialize(INavigationParameter parameter)
        {
            CanBeActivated = true;
            InitializePatientStammdatenParameter();
            InitializePatientTherapyParameter();
        }

        private void InitializePatientStammdatenParameter()
        {
            PatientStammdatenParameter = new NavigationParameter(Regions.OverView, typeof (PatientStammdatenViewModel));
            PatientStammdatenParameter.SetParameter(new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("Patient", new PatientViewModel(
                                                                new PatientArticleWorkItem(null),
                                                                new PatientHospitalWorkItem(null),
                                                                new PatientInsuranceWorkItem(null),
                                                                new PatientTherapyWorkItem())
                {
                    FirstName = "Albert",
                    LastName = "Slupianek",
                    Birthday = new DateTime(1982, 5, 20),
                    Id = Guid.NewGuid()
                })
            });
        }

        private void InitializePatientTherapyParameter()
        {
            NavigationParameter patientTherapyActionMenuParameter = new NavigationParameter(Regions.AppActionMenuView, typeof (PatientTherapyActionMenuViewModel));

            PatientViewModel patient = new PatientViewModel(
                new PatientArticleWorkItem(null),
                new PatientHospitalWorkItem(null),
                new PatientInsuranceWorkItem(null),
                new PatientTherapyWorkItem())
            {
                FirstName = "Peter",
                LastName = "Lustig",
                Birthday = new DateTime(1934, 7, 9),
                Id = Guid.NewGuid()
            };

            patientTherapyActionMenuParameter.SetParameter(new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("Patient", patient)
            });

            NavigationParameter patientTherapyListOverviewParameter = new NavigationParameter(Regions.OverView, typeof (PatientTherapyListOverviewViewModel));
            patientTherapyListOverviewParameter.SetParameter(new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>("Patient", patient)
            });

            MergedNavigationParameter mergedPatientTherapyParameter = new MergedNavigationParameter();
            mergedPatientTherapyParameter.Add(patientTherapyListOverviewParameter);
            mergedPatientTherapyParameter.Add(patientTherapyActionMenuParameter);

            PatientTherapyParameter = mergedPatientTherapyParameter;
        }
    }
}