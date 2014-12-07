using SoftCareManager.Contracts.Model;
using SoftCareManager.Contracts.WorkItems;

namespace SoftCareManager.Business.WorkItems
{
    internal class NullWorkItem : IWorkItem
    {
        public IModel Model { get; set; }

        public IAppController RootWorkItem { get; set; }
    }
}