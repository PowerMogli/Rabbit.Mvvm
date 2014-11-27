using Appccelerate.EventBroker;
using Appccelerate.EventBroker.Handlers;
using SoftCareManager.Common;
using SoftCareManager.Contracts.Application.Navigation;
using SoftCareManager.Contracts.General;
using SoftCareManager.Contracts.Services;
using SoftCareManager.Contracts.ViewModel;
using SoftCareManager.Contracts.WorkItems;
using System;
using System.ComponentModel.Composition;

namespace SoftCareManager.ViewModel.Application
{
    public class AppShellContainerViewModel : ViewModelBase
    {
        private int _shellId;
        private bool _isTouch;
        private readonly IAppController _appController;

        public bool IsTouch
        {
            get { return _isTouch; }
            set
            {
                _isTouch = value;
                RaisePropertyChanged();
            }
        }

        public int ShellId
        {
            get { return _shellId; }
            set
            {
                _shellId = value;
                RaisePropertyChanged();
            }
        }

        public AppShellSwitch AppShellSwitch { get; private set; }

        [ImportingConstructor]
        public AppShellContainerViewModel(IAppController appController, AppShellSwitch appShellSwitch)
        {
            _appController = appController;
            AppShellSwitch = appShellSwitch;

            _appController.RegisterOnEventBroker(this);

            _appController.Execute();
        }

        public bool CanExecuteGeneral(object obj)
        {
            return AppShellSwitch != null && AppShellSwitch.ShellViewModel != null;
        }

        public void ChangeSkin(object obj)
        {
            var skinService = _appController.GetService<ISkinService>();
            skinService.ChangeSkin(!IsTouch);

            // Change of IsTouch-Property should be applied 
            // after ResourceDictionary gets updated.
            IsTouch = !IsTouch;

            ChangePlatformControls(Regions.AppMenuView, typeof(AppMenuViewModel));
            ChangePlatformControls(Regions.AppInformationView, typeof(AppInformationViewModel));
        }

        private void ChangePlatformControls(string regionName, Type viewModelType)
        {
            var navigationParameter = new NavigationParameter(regionName, viewModelType);
            navigationParameter.SetStayVisible(true);

            NavigateTo(navigationParameter);
        }

        public void ToogleAppShellMenu()
        {
            NavigateTo(new NavigationParameter(Regions.AppShellMenuView, typeof(AppShellMenuViewModel)));
        }

        public void NavigateTo(object parameter)
        {
            var navigationParameter = parameter as INavigationParameter;
            if (navigationParameter == null)
            {
                return;
            }

            navigationParameter.SetShellId(_shellId);

            var navigationService = _appController.GetService<INavigationProxy>();
            navigationService.RequestNavigation(navigationParameter, _appController);
        }

        public bool CanChangeShell(object parameter)
        {
            return _shellId != Convert.ToInt32(parameter);
        }

        public void ChangeShell(object parameter)
        {
            _shellId = Convert.ToInt32(parameter);

            AppShellSwitch.Switch(_shellId);
        }


        [EventSubscription(EventTopics.AppControllerInitialized, typeof(OnUserInterface))]
        public void OnInitialized(EventArgs e)
        {
            ChangeShell(0);

            NavigateTo(new NavigationParameter(Regions.AppMenuView, typeof(AppMenuViewModel)));
        }
    }
}