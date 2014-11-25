using Appccelerate.EventBroker;
using SoftCareManager.Contracts.General;
using SoftCareManager.Contracts.Model;
using SoftCareManager.Contracts.Services;
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
        private Service.IServiceProvider serviceProvider;

        private IWorkItemProvider workItemProvider;

        private IEventBroker eventBroker;

        // Property injection - daher weiterhin testbar!
        public IEventBroker EventBroker
        {
            get { return eventBroker ?? (eventBroker = new EventBroker()); }
            set { eventBroker = value; }
        }

        /// <summary>
        /// Occurs when the pong UI from UI thread is fired.
        /// </summary>
        [EventPublication(EventTopics.AppControllerInitialized)]
        public event EventHandler InitializationCompleted;

        [ImportingConstructor]
        public AppController(Service.IServiceProvider serviceProvider, IWorkItemProvider workItemProvider)
        {
            this.serviceProvider = serviceProvider;
            this.workItemProvider = workItemProvider;

            EventBroker.Register(this);
        }

        public async void Execute()
        {
            await Task.Delay(2000);

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

            eventBroker.Register(item);
        }
    }
}