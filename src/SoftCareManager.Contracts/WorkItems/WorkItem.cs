using SoftCareManager.Contracts.Model;
using SoftCareManager.Contracts.ViewModel;

namespace SoftCareManager.Contracts.WorkItems
{
    public abstract class WorkItem<TModel> : IWorkItem
        where TModel : IModel
    {
        public IModel Model { get; set; }

        public IAppController RootWorkItem { get; set; }
    }
}