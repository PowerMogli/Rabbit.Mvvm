using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Appccelerate.EventBroker;
using Appccelerate.EventBroker.Handlers;
using SoftCareManager.Common;
using SoftCareManager.Contracts.Application.Navigation;
using SoftCareManager.Contracts.General;
using SoftCareManager.Contracts.Services;
using SoftCareManager.Contracts.ViewModel;
using SoftCareManager.Contracts.WorkItems;

namespace SoftCareManager.ViewModel.Application
{
    [Export(ViewModelName.DesktopMainViewModel, typeof(ViewModelBase))]
    public class AppShellContainerViewModel : ViewModelBase
    {
        private ICommand _viewLoaded;
        private ICommand _navigateTo;
        private ICommand _changeSkin;

        private Action _onInitializationCompleted;

        private readonly IAppController _appController;

        public ICommand ViewLoaded
        {
            get { return _viewLoaded ?? (_viewLoaded = new DelegateCommand(OnViewLoaded)); }
        }

        public ICommand NavigateTo
        {
            get { return _navigateTo ?? (_navigateTo = new DelegateCommand(OnNavigateTo)); }
        }

        public ICommand ChangeSkin
        {
            get { return _changeSkin ?? (_changeSkin = new DelegateCommand(OnChangeSkin)); }
        }

        [ImportingConstructor]
        public AppShellContainerViewModel(IAppController appController)
        {
            _appController = appController;

            appController.RegisterOnEventBroker(this);
        }

        private void OnChangeSkin(object obj)
        {
            var skinService = _appController.GetService<ISkinService>();
            skinService.ChangeSkin();
        }

        private void OnNavigateTo(object parameter)
        {
            var navigationService = _appController.GetService<INavigationProxy>();
            navigationService.RequestNavigation(parameter as INavigationParameter, _appController);
        }

        private void OnViewLoaded(object obj)
        {
            _appController.Execute();

            _onInitializationCompleted = obj as Action;
        }

        /// <summary>
        /// Handles a ping.
        /// </summary>
        /// <param name="e">The event arguments</param>
        [EventSubscription(EventTopics.AppControllerInitialized, typeof(OnUserInterface))]
        public void OnInitialized(EventArgs e)
        {
            _onInitializationCompleted();
        }
    }
}
