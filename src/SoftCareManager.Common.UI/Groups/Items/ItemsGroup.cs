using SoftCareManager.Common.UI.Views.Patient;
using SoftCareManager.Contracts.Groups.Base;
using SoftCareManager.Contracts.Groups.Items;
using System.Windows;
using System.Windows.Data;

namespace SoftCareManager.Common.UI.Groups.Items
{
    public class ItemsGroup : IItemsGroup
    {
        private const string bindingPath = "Patients";

        public string Name { get; set; }

        public DependencyObject ItemsSubscriber { get; set; }

        public IItemsPublisher ItemsPublisher { get; set; }

        public void Bind()
        {
            if (ItemsSubscriber != null
                && ItemsPublisher != null)
            {
                BindingOperations.SetBinding(ItemsSubscriber, BasePatientActionMenu.ItemsProperty, new Binding(bindingPath) { Source = ItemsPublisher, Mode = BindingMode.OneWay });
            }
        }

        public void AddSubscriber(DependencyObject dependencyObject)
        {
            IItemsSubscriber selectionSubscriber = dependencyObject as IItemsSubscriber;
            if (selectionSubscriber != null)
            {
                ItemsSubscriber = dependencyObject;
            }
        }

        public void AddPublisher(IGroupSource groupSource)
        {
            var itemsGroupSource = groupSource as IItemsGroupSource;
            if (itemsGroupSource == null
                && itemsGroupSource.ItemsPublisher == null)
            {
                return;
            }

            ItemsPublisher = itemsGroupSource.ItemsPublisher;
        }
    }
}