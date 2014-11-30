using SoftCareManager.Common.UI.Groups.Base;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;

namespace SoftCareManager.Common.UI.Groups.Items
{
    [Export(typeof(IItemsGroupManager))]
    public class ItemsGroupManager : BaseGroupManager<ItemsGroup, ItemsGroupSource>, IItemsGroupManager
    {
        public static IItemsGroupManager GetItemsGroupManager(DependencyObject obj)
        {
            return (IItemsGroupManager)obj.GetValue(ItemsGroupManagerProperty);
        }

        public static void SetItemsGroupManager(DependencyObject obj, IItemsGroupManager value)
        {
            obj.SetValue(ItemsGroupManagerProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectionGroupManager.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsGroupManagerProperty =
            DependencyProperty.RegisterAttached("ItemsGroupManager", typeof(IItemsGroupManager), typeof(ItemsGroupManager), new PropertyMetadata(null));

        public static string GetItemsGroupName(DependencyObject obj)
        {
            return (string)obj.GetValue(ItemsGroupNameProperty);
        }

        public static void SetItemsGroupName(DependencyObject obj, string value)
        {
            obj.SetValue(ItemsGroupNameProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectionGroupName. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsGroupNameProperty =
            DependencyProperty.RegisterAttached("ItemsGroupName", typeof(string), typeof(ItemsGroupManager), new PropertyMetadata(string.Empty, OnItemsGroupNamePropertyChanged));

        private static void OnItemsGroupNamePropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var itemsGroupManager = GetItemsGroupManager(Application.Current.MainWindow);
            var itemSourceGroup = itemsGroupManager.AddSubscriber(e.NewValue as string, dependencyObject);

            itemSourceGroup.Bind();
        }

        public static ItemsGroupSource GetItemsGroupSource(DependencyObject obj)
        {
            return (ItemsGroupSource)obj.GetValue(ItemsGroupSourceProperty);
        }

        public static void SetItemsGroupSource(DependencyObject obj, ItemsGroupSource value)
        {
            obj.SetValue(ItemsGroupSourceProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectionGroupSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsGroupSourceProperty =
            DependencyProperty.RegisterAttached("ItemsGroupSource", typeof(ItemsGroupSource), typeof(ItemsGroupManager), new PropertyMetadata(null, OnItemsGroupSourcePropertyChanged));

        private static void OnItemsGroupSourcePropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var itemsGroupSource = e.NewValue as ItemsGroupSource;
            if (itemsGroupSource == null)
            {
                return;
            }

            var itemsGroupManager = GetItemsGroupManager(Application.Current.MainWindow);
            var itemsGroup = itemsGroupManager.AddPublisher(itemsGroupSource);

            itemsGroup.Bind();
        }
    }
}