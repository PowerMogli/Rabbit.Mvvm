using SoftCareManager.Contracts.Model;

namespace SoftCareManager.Contracts.Groups.Selection
{
    public interface ISelectionPublisher<TModel> : ISelectionPublisher
        where TModel : IModel
    {
        TModel SelectedItem { get; set; }
    }

    public interface ISelectionPublisher
    {
    }
}