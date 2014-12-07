using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;

using SoftCareManager.Common.UI.Groups.Base;
using SoftCareManager.Common.UI.Views.Patient;
using SoftCareManager.Contracts.Groups.Base;
using SoftCareManager.Contracts.Groups.Items;

namespace SoftCareManager.Common.UI.Groups.Items
{
    public class ItemsGroup : IGroup
    {
        private const string BindingPath = "Items";

        private readonly List<DependencyObject> _itemsSubscriber;
        private IItemsPublisher _itemsPublisher;

        public ItemsGroup()
        {
            _itemsSubscriber = new List<DependencyObject>();
        }

        public string Name { get; set; }

        public void AddPublisher(IGroupSource groupSource)
        {
            IItemsGroupSource itemsGroupSource = groupSource as IItemsGroupSource;
            if (itemsGroupSource == null
                || itemsGroupSource.ItemsPublisher == null)
            {
                return;
            }

            _itemsPublisher = itemsGroupSource.ItemsPublisher;
        }

        public void AddSubscriber(DependencyObject dependencyObject)
        {
            IItemsSubscriber subscriber = dependencyObject as IItemsSubscriber;
            if (subscriber != null)
            {
                _itemsSubscriber.Add(dependencyObject);
            }
        }

        public void Bind()
        {
            if (_itemsSubscriber == null
                || _itemsPublisher == null)
            {
                return;
            }

            for (var index = _itemsSubscriber.Count - 1; index >= 0; index--)
            {
                BindingOperations.ClearBinding(_itemsSubscriber[index], BaseActionMenu2.ItemsProperty);
                BindingOperations.SetBinding(_itemsSubscriber[index], BaseActionMenu2.ItemsProperty, new Binding(BindingPath)
                {
                    Source = _itemsPublisher,
                    Mode = BindingMode.OneWay,
                });

                _itemsSubscriber.RemoveAt(index);
            }
        }
    }
}