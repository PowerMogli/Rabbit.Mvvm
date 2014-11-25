using SoftCareManager.Contracts.Model;

namespace SoftCareManager.Contracts.WorkItems
{
    public interface IWorkItem
    {
        IModel Model { get; set; }

        IAppController RootWorkItem { get; set; }
    }
}