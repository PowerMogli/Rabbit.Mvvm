using SoftCareManager.Common;
using SoftCareManager.Contracts.Application.Navigation;
using SoftCareManager.Contracts.General;
using SoftCareManager.Contracts.ViewModel;

namespace SoftCareManager.ViewModel.Application
{
    public class AppMenuViewModel : ViewModelBase
    {
        public INavigationParameter OverViewParameter { get; set; }

        protected override void InnerInitialize(INavigationParameter navigationParameter)
        {
            CanBeActivated = true;
            OverViewParameter = new NavigationParameter(Regions.AppInformationView, typeof (AppInformationViewModel));

            IsInitialized = true;
        }
    }
}