using System.Collections.ObjectModel;

namespace SoftCareManager.Contracts.Groups.Items
{
    public interface IItemsSubscriber
    {
        ObservableCollection<object> Items { get; set; }
    }
}