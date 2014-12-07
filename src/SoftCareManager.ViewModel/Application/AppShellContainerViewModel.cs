using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;

using Appccelerate.EventBroker;
using Appccelerate.EventBroker.Handlers;

using SoftCareManager.Common;
using SoftCareManager.Contracts;
using SoftCareManager.Contracts.Application.Navigation;
using SoftCareManager.Contracts.Authorization;
using SoftCareManager.Contracts.General;
using SoftCareManager.Contracts.Services;
using SoftCareManager.Contracts.ViewModel;
using SoftCareManager.Contracts.WorkItems;

namespace SoftCareManager.ViewModel.Application
{
    public class AppShellContainerViewModel : ViewModelBase
    {
        private readonly IAppController _appController;
        private bool _isTouch;
        private int _shellId;

        [ImportingConstructor]
        public AppShellContainerViewModel(IAppController appController, AppShellSwitch appShellSwitch)
        {
            _appController = appController;
            AppShellSwitch = appShellSwitch;

            _appController.RegisterOnEventBroker(this);

            _appController.Execute();
        }

        public AppShellSwitch AppShellSwitch { get; private set; }

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

        public IUIVisibilityBinder VisibilityBinder { get; set; }

        public bool CanChangeShell(object parameter)
        {
            return _shellId != Convert.ToInt32(parameter);
        }

        public bool CanExecuteGeneral(object obj)
        {
            return AppShellSwitch != null && AppShellSwitch.ShellViewModel != null;
        }

        public void ChangeShell(object parameter)
        {
            _shellId = Convert.ToInt32(parameter);

            AppShellSwitch.Switch(_shellId);
        }

        public void ChangeSkin(object obj)
        {
            ISkinService skinService = _appController.GetService<ISkinService>();
            skinService.ChangeSkin(!IsTouch);

            // Change of IsTouch-Property should be applied 
            // after ResourceDictionary gets updated.
            IsTouch = !IsTouch;

            ChangePlatformControls(Regions.AppMenuView, typeof(AppMenuViewModel));
            ChangePlatformControls(Regions.AppInformationView, typeof(AppInformationViewModel));
        }

        public void NavigateTo(object parameter)
        {
            INavigationParameter navigationParameter = parameter as INavigationParameter;
            if (navigationParameter == null)
            {
                return;
            }

            navigationParameter.SetShellId(_shellId);

            INavigationProxy navigationService = _appController.GetService<INavigationProxy>();
            navigationService.RequestNavigation(navigationParameter, _appController);
        }


        [EventSubscription(EventTopics.AppControllerInitialized, typeof(OnUserInterface))]
        public void OnInitialized(EventArgs e)
        {
            InitializeVisibilityBinder();

            ChangeShell(0);

            NavigateTo(new NavigationParameter(Regions.AppMenuView, typeof(AppMenuViewModel)));
        }

        public void ToogleAppShellMenu()
        {
            NavigateTo(new NavigationParameter(Regions.AppShellMenuView, typeof(AppShellMenuViewModel)));
        }

        private void ChangePlatformControls(string regionName, Type viewModelType)
        {
            NavigationParameter navigationParameter = new NavigationParameter(regionName, viewModelType);
            navigationParameter.SetStayVisible(true);

            NavigateTo(navigationParameter);
        }

        private void InitializeVisibilityBinder()
        {
            IUser user = new User("Albert", Role.ItDeveloper,
                Right.AddPatient
                | Right.AddTherapy
                | Right.DeletePatient
                | Right.DeleteTherapy
                | Right.EditPatient
                | Right.EditTherapy);

            VisibilityBinder.Initialize(CreateRightRoleRelations(), user);
        }

        private static IEnumerable<RoleRightRelation> CreateRightRoleRelations()
        {
            IList<RoleRightRelation> relations = new List<RoleRightRelation>();

            relations.Add(new RoleRightRelation(Role.CareManager | Role.TherapyConsult | Role.ItDeveloper, Right.AddPatient));
            relations.Add(new RoleRightRelation(Role.ItService | Role.TherapyConsult, Right.EditTherapy));
            relations.Add(new RoleRightRelation(Role.TherapyConsult, Right.DeleteTherapy));

            return new ReadOnlyCollection<RoleRightRelation>(relations);
        }
    }
}