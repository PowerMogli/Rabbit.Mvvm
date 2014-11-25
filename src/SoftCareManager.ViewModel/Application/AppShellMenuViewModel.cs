using SoftCareManager.Common;
using SoftCareManager.Contracts.Application.Navigation;
using SoftCareManager.Contracts.General;
using SoftCareManager.Contracts.ViewModel;
using SoftCareManager.ViewModel.Patient;

namespace SoftCareManager.ViewModel.Application
{
    public class AppShellMenuViewModel : ViewModelBase
    {
        public IMergedNavigationParameter PatientParameter { get; set; }

        public override void Initialize(INavigationParameter navigationParameter)
        {
            PatientParameter = new MergedNavigationParameter();
            PatientParameter.Add(new NavigationParameter(Regions.OverView, typeof(PatientOverviewViewModel)));
            PatientParameter.Add(new NavigationParameter(Regions.SubMenuView, typeof(PatientSubMenuViewModel)));

            CanBeActivated = !CanBeActivated;
        }
    }
}