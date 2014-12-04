using SoftCareManager.Common.UI.Groups.Base;
using SoftCareManager.Common.UI.Views.Patient;
using SoftCareManager.Contracts.Groups.Base;
using SoftCareManager.Contracts.Groups.Items;
using System.Windows;
using System.Windows.Data;

namespace SoftCareManager.Common.UI.Groups.Items
{
    public class ItemsGroup : IGroup
    {
        private const string BindingPath = "Items";

        public string Name { get; set; }

        private DependencyObject _itemsSubscriber;

        private IItemsPublisher _itemsPublisher;

        public void Bind()
        {
            if (_itemsSubscriber == null || _itemsPublisher == null)
            {
                return;
            }

            BindingOperations.ClearBinding(_itemsSubscriber, BaseActionMenu2.ItemsProperty);
            BindingOperations.SetBinding(_itemsSubscriber, BaseActionMenu2.ItemsProperty, new Binding(BindingPath)
            {
                Source = _itemsPublisher,
                Mode = BindingMode.OneWay,
            });
        }

        public void AddSubscriber(DependencyObject dependencyObject)
        {
            IItemsSubscriber subscriber = dependencyObject as IItemsSubscriber;
            if (subscriber != null)
            {
                _itemsSubscriber = dependencyObject;
            }
        }

        public void AddPublisher(IGroupSource groupSource)
        {
            var itemsGroupSource = groupSource as IItemsGroupSource;
            if (itemsGroupSource == null
                || itemsGroupSource.ItemsPublisher == null)
            {
                return;
            }

            _itemsPublisher = itemsGroupSource.ItemsPublisher;
        }
    }
}
