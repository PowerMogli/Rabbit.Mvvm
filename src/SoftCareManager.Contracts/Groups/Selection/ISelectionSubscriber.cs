using SoftCareManager.Contracts.Model;

namespace SoftCareManager.Contracts.Groups.Selection
{
    public interface ISelectionSubscriber<TModel> : ISelectionSubscriber
        where TModel : IModel
    {
        new TModel SelectedItem { get; set; }
    }

    public interface ISelectionSubscriber
    {
        object SelectedItem { get; set; }
    }
}