using Appccelerate.EventBroker;
using SoftCareManager.Contracts;
using SoftCareManager.Contracts.General;
using SoftCareManager.Contracts.Services;
using SoftCareManager.Contracts.ViewModel;
using SoftCareManager.Contracts.WorkItems;
using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Service = SoftCareManager.Contracts.Services;

namespace SoftCareManager.Business.WorkItems
{
    [Export(typeof(IAppController))]
    public class AppController : IAppController
    {
        private readonly Service.IServiceProvider serviceProvider;

        private readonly IWorkItemProvider workItemProvider;

        private IEventBroker eventBroker;

        private readonly IObjectBuilder objectBuilder;

        // Property injection - daher weiterhin testbar!
        public IEventBroker EventBroker
        {
            get { return eventBroker ?? (eventBroker = new EventBroker()); }
            set { eventBroker = value; }
        }

        [EventPublication(EventTopics.AppControllerInitialized)]
        public event EventHandler InitializationCompleted;

        [ImportingConstructor]
        public AppController(Service.IServiceProvider serviceProvider, IWorkItemProvider workItemProvider, IObjectBuilder objectBuilder)
        {
            this.serviceProvider = serviceProvider;
            this.workItemProvider = workItemProvider;
            this.objectBuilder = objectBuilder;

            RegisterOnEventBroker(this);
        }

        public async void Execute()
        {
            await Task.Delay(100);

            OnInitializationCompleted();
        }

        private void OnInitializationCompleted()
        {
            var handler = InitializationCompleted;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public TService GetService<TService>()
            where TService : IService
        {
            var service = serviceProvider.GetService<TService>();
            eventBroker.Register(service);

            return service;
        }

        public object GetService(Type serviceType)
        {
            var service = serviceProvider.GetService(serviceType);
            eventBroker.Register(service);

            return service;
        }

        public TWorkItem GetWorkItem<TWorkItem>()
            where TWorkItem : IWorkItem
        {
            var workItem = workItemProvider.Get<TWorkItem>();
            workItem.RootWorkItem = this;

            eventBroker.Register(workItem);

            return workItem;
        }

        public object GetWorkItem(Type workItemType)
        {
            var workItem = workItemProvider.Get(workItemType) as IWorkItem;
            workItem.RootWorkItem = this;

            eventBroker.Register(workItem);

            return workItem;
        }

        public void RegisterOnEventBroker(object item)
        {
            if (item == null)
            {
                return;
            }

            EventBroker.Register(item);
        }

        public ViewModelBase BuildViewModel(Type viewModelType)
        {
            return objectBuilder.Build(viewModelType, this) as ViewModelBase;
        }
    }
}