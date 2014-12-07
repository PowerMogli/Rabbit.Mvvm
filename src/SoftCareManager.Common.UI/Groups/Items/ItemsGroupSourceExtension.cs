using SoftCareManager.Common.UI.Groups.Base;
using SoftCareManager.Contracts.Groups.Items;
using System;

namespace SoftCareManager.Common.UI.Groups.Items
{
    public class ItemsGroupSourceExtension : BaseGroupSourceExtension<ItemsGroupSource>
    {
        public ItemsGroupSourceExtension()
        {
            _groupSource = new Lazy<ItemsGroupSource>(() => new ItemsGroupSource(GroupName, _dataContext as IItemsPublisher));
        }

        public ItemsGroupSourceExtension(string groupName)
            : this()
        {
            GroupName = groupName;
        }

        protected override void OnDataContextFound()
        {
            ItemsGroupManager.SetItemsGroupSource(_targetObject, _groupSource.Value);
        }
    }
}