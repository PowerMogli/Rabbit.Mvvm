using SoftCareManager.Contracts.Application.Navigation;
using SoftCareManager.Contracts.ViewModel;

namespace SoftCareManager.ViewModel.Patient
{
    public class PatientStammdatenViewModel : ViewModelBase
    {
        private PatientViewModel patient;

        public PatientViewModel Patient
        {
            get { return patient; }
            set
            {
                patient = value;
                RaisePropertyChanged();
            }
        }

        protected override void InnerInitialize(INavigationParameter parameter)
        {
            CanBeActivated = true;
            Patient = parameter.Parameter["Patient"] as PatientViewModel;
        }
    }
}