using SoftCareManager.Contracts.Application.Navigation;
using SoftCareManager.Contracts.ViewModel;

namespace SoftCareManager.ViewModel.Application
{
    public class AppInformationViewModel : ViewModelBase
    {
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