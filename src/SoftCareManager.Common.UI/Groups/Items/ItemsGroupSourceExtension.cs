using System;

using SoftCareManager.Common.UI.Groups.Base;
using SoftCareManager.Contracts.Groups.Items;

namespace SoftCareManager.Common.UI.Groups.Items
{
    public class ItemsGroupSourceExtension : BaseGroupSourceExtension<ItemsGroupSource>
    {
        public ItemsGroupSourceExtension()
            : this(string.Empty)
        {
        }

        public ItemsGroupSourceExtension(string groupName)
            : base(groupName)
        {
            GroupSource = new Lazy<ItemsGroupSource>(() => new ItemsGroupSource(GroupName, DataContext as IItemsPublisher));
        }

        protected override void OnDataContextFound()
        {
            ItemsGroupManager.SetItemsGroupSource(TargetObject, GroupSource.Value);
        }
    }
}