using SoftCareManager.Common.UI.Groups.Base;
using System.Windows;

namespace SoftCareManager.Common.UI.Groups.Items
{
    public interface IItemsGroupManager : IBaseGroupManager<ItemsGroup, ItemsGroupSource>
    {
        ItemsGroup AddPublisher(ItemsGroupSource itemsGroupSource);

        ItemsGroup AddSubscriber(string itemSourceGroupName, DependencyObject dependencyObjects);
    }
}