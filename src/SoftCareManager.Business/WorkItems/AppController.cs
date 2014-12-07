using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

using Appccelerate.EventBroker;

using SoftCareManager.Contracts;
using SoftCareManager.Contracts.General;
using SoftCareManager.Contracts.ViewModel;
using SoftCareManager.Contracts.WorkItems;

using Service = SoftCareManager.Contracts.Services;

namespace SoftCareManager.Business.WorkItems
{
    [Export(typeof (IAppController))]
    public class AppController : IAppController
    {
        private readonly IObjectBuilder _objectBuilder;
        private readonly Service.IServiceProvider _serviceProvider;

        private readonly IWorkItemProvider _workItemProvider;

        private IEventBroker _eventBroker;

        [ImportingConstructor]
        public AppController(Service.IServiceProvider serviceProvider, IWorkItemProvider workItemProvider, IObjectBuilder objectBuilder)
        {
            _serviceProvider = serviceProvider;
            _workItemProvider = workItemProvider;
            _objectBuilder = objectBuilder;

            RegisterOnEventBroker(this);
        }

        [EventPublication(EventTopics.AppControllerInitialized)]
        public event EventHandler InitializationCompleted;

        public IEventBroker EventBroker
        {
            get { return _eventBroker ?? (_eventBroker = new EventBroker()); }
            set { _eventBroker = value; }
        }

        public ViewModelBase BuildViewModel(Type viewModelType)
        {
            return _objectBuilder.Build(viewModelType, this) as ViewModelBase;
        }

        public async void Execute()
        {
            await Task.Delay(100);

            OnInitializationCompleted();
        }

        public TService GetService<TService>()
            where TService : Service.IService
        {
            TService service = _serviceProvider.GetService<TService>();
            EventBroker.Register(service);

            return service;
        }

        public object GetService(Type serviceType)
        {
            object service = _serviceProvider.GetService(serviceType);
            EventBroker.Register(service);

            return service;
        }

        public TWorkItem GetWorkItem<TWorkItem>()
            where TWorkItem : IWorkItem
        {
            TWorkItem workItem = _workItemProvider.Get<TWorkItem>();
            workItem.RootWorkItem = this;

            EventBroker.Register(workItem);

            return workItem;
        }

        public object GetWorkItem(Type workItemType)
        {
            IWorkItem workItem = _workItemProvider.Get(workItemType) as IWorkItem;
            if (workItem == null)
            {
                return new NullWorkItem();
            }

            workItem.RootWorkItem = this;
            EventBroker.Register(workItem);

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

        private void OnInitializationCompleted()
        {
            EventHandler handler = InitializationCompleted;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}