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
using System.Windows.Input;

namespace SoftCareManager.ViewModel.Application
{
    [Export(ViewModelName.DesktopMainViewModel, typeof(ViewModelBase))]
    public class AppShellContainerViewModel : ViewModelBase
    {
        private ICommand viewLoaded;
        private ICommand navigateTo;
        private ICommand changeSkin;

        private Action OnInitializationCompleted;

        private IAppController appController;

        public ICommand ViewLoaded { get { return viewLoaded ?? (viewLoaded = new DelegateCommand(OnViewLoaded)); } }

        public ICommand NavigateTo { get { return navigateTo ?? (navigateTo = new DelegateCommand(OnNavigateTo)); } }

        public ICommand ChangeSkin { get { return changeSkin ?? (changeSkin = new DelegateCommand(OnChangeSkin)); } }

        [ImportingConstructor]
        public AppShellContainerViewModel(IAppController appController)
        {
            this.appController = appController;

            appController.RegisterOnEventBroker(this);
        }

        private void OnChangeSkin(object obj)
        {
            ISkinService skinService = appController.GetService<ISkinService>();
            skinService.ChangeSkin();
        }

        private void OnNavigateTo(object parameter)
        {
            INavigationProxy navigationService = appController.GetService<INavigationProxy>();
            navigationService.RequestNavigation(parameter as INavigationParameter, appController);
        }

        private void OnViewLoaded(object obj)
        {
            appController.Execute();

            OnInitializationCompleted = obj as Action;
        }

        /// <summary>
        /// Handles a ping.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments</param>
        [EventSubscription(EventTopics.AppControllerInitialized, typeof(OnUserInterface))]
        public void OnInitialized(EventArgs e)
        {
            OnInitializationCompleted();
        }
    }
}