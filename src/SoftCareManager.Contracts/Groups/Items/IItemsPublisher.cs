using System.Collections.ObjectModel;

namespace SoftCareManager.Contracts.Groups.Items
{
    public interface IItemsPublisher
    {
        ObservableCollection<object> Items { get; set; }
    }
}