using SoftCareManager.Business.WorkItems.Patient;
using SoftCareManager.Common;
using SoftCareManager.Contracts.Application.Navigation;
using SoftCareManager.Contracts.General;
using SoftCareManager.Contracts.ViewModel;
using System;
using System.Collections.Generic;

namespace SoftCareManager.ViewModel.Patient
{
    public class PatientSubMenuViewModel : ViewModelBase, INavigationAware
    {
        private INavigationParameter patientStammdatenParameter;
        public INavigationParameter PatientStammdatenParameter
        {
            get { return patientStammdatenParameter; }
            set
            {
                patientStammdatenParameter = value;
                RaisePropertyChanged();
            }
        }
        public INavigationParameter PatientDokumentationParameter { get; set; }
        public INavigationParameter PatientTherapyParameter { get; set; }

        protected override void InnerInitialize(INavigationParameter parameter)
        {
            CanBeActivated = true;
            PatientStammdatenParameter = new NavigationParameter(Regions.OverView, typeof(PatientStammdatenViewModel));
            PatientStammdatenParameter.SetParameter(new KeyValuePair<string, object>[] 
            { 
                new KeyValuePair<string, object>("Patient", new PatientViewModel(new PatientArticleWorkItem(null), new PatientHospitalWorkItem(null), new PatientInsuranceWorkItem(null)) 
                {
                    FirstName = "Albert", 
                    LastName = "Slupianek", 
                    Birthday = new DateTime(1982, 5, 20), 
                    Id = Guid.NewGuid()
                })
            });
        }

        public bool NavigateFrom()
        {
            return true;
        }

        public void NavigateTo()
        {

        }
    }
}