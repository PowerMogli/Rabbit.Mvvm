using SoftCareManager.Contracts.Application.Navigation;
using SoftCareManager.Contracts.ViewModel;
using SoftCareManager.Contracts.WorkItems;

namespace SoftCareManager.ViewModel.Application
{
    public class AppInformationViewModel : ViewModelBase
    {
        public AppInformationViewModel()
        {

        }

        protected override void InnerInitialize(INavigationParameter navigationParameter)
        {
            if (navigationParameter.StayVisible)
            {
                return;
            }

            CanBeActivated = !CanBeActivated;
        }
    }
}