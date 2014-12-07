using SoftCareManager.Contracts.Groups.Items;

namespace SoftCareManager.Common.UI.Groups.Items
{
    public class ItemsGroupSource : IItemsGroupSource
    {
        public ItemsGroupSource(string name, IItemsPublisher itemsPublisher)
        {
            GroupName = name;
            ItemsPublisher = itemsPublisher;
        }

        public string GroupName { get; private set; }

        public IItemsPublisher ItemsPublisher { get; private set; }
    }
}