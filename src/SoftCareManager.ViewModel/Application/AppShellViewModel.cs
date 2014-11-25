using SoftCareManager.Contracts.ViewModel;

namespace SoftCareManager.ViewModel.Application
{
    public class AppShellViewModel : ViewModelBase
    {
        private int _shellId;
        public int ShellId
        {
            get { return _shellId; }
            set
            {
                _shellId = value;
                RaisePropertyChanged();
            }
        }

        public AppShellViewModel(int shellId)
        {
            CanBeActivated = true;
            _shellId = shellId;
        }
    }
}