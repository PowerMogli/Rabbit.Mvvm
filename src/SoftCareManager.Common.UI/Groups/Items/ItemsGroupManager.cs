using System.ComponentModel.Composition;
using System.Windows;

using SoftCareManager.Common.UI.Groups.Base;

namespace SoftCareManager.Common.UI.Groups.Items
{
    [Export(typeof (IBaseGroupManager<ItemsGroup, ItemsGroupSource>))]
    public class ItemsGroupManager : BaseGroupManager<ItemsGroup, ItemsGroupSource>
    {
        public static readonly DependencyProperty ItemsGroupManagerProperty =
            DependencyProperty.RegisterAttached("ItemsGroupManager",
                                                typeof (IBaseGroupManager<ItemsGroup, ItemsGroupSource>),
                                                typeof (ItemsGroupManager),
                                                new PropertyMetadata(null));

        public static readonly DependencyProperty ItemsGroupNameProperty =
            DependencyProperty.RegisterAttached("ItemsGroupName",
                                                typeof (string),
                                                typeof (ItemsGroupManager),
                                                new PropertyMetadata(string.Empty, OnItemsGroupNamePropertyChanged));

        public static readonly DependencyProperty ItemsGroupSourceProperty =
            DependencyProperty.RegisterAttached("ItemsGroupSource",
                                                typeof (ItemsGroupSource),
                                                typeof (ItemsGroupManager),
                                                new PropertyMetadata(null, OnItemsGroupSourcePropertyChanged));

        public static IBaseGroupManager<ItemsGroup, ItemsGroupSource> GetItemsGroupManager(DependencyObject obj)
        {
            return (IBaseGroupManager<ItemsGroup, ItemsGroupSource>)obj.GetValue(ItemsGroupManagerProperty);
        }

        public static string GetItemsGroupName(DependencyObject obj)
        {
            return (string)obj.GetValue(ItemsGroupNameProperty);
        }

        public static ItemsGroupSource GetItemsGroupSource(DependencyObject obj)
        {
            return (ItemsGroupSource)obj.GetValue(ItemsGroupSourceProperty);
        }

        public static void SetItemsGroupManager(DependencyObject obj,
                                                IBaseGroupManager<ItemsGroup, ItemsGroupSource> value)
        {
            obj.SetValue(ItemsGroupManagerProperty, value);
        }

        public static void SetItemsGroupName(DependencyObject obj, string value)
        {
            obj.SetValue(ItemsGroupNameProperty, value);
        }

        public static void SetItemsGroupSource(DependencyObject obj, ItemsGroupSource value)
        {
            obj.SetValue(ItemsGroupSourceProperty, value);
        }

        private static void OnItemsGroupNamePropertyChanged(DependencyObject dependencyObject,
                                                            DependencyPropertyChangedEventArgs e)
        {
            IBaseGroupManager<ItemsGroup, ItemsGroupSource> groupManager =
                GetItemsGroupManager(Application.Current.MainWindow);
            ItemsGroup group = groupManager.AddSubscriber(e.NewValue as string, dependencyObject);

            group.Bind();
        }

        private static void OnItemsGroupSourcePropertyChanged(DependencyObject dependencyObject,
                                                              DependencyPropertyChangedEventArgs e)
        {
            ItemsGroupSource itemsGroupSource = e.NewValue as ItemsGroupSource;
            if (itemsGroupSource == null)
            {
                return;
            }

            IBaseGroupManager<ItemsGroup, ItemsGroupSource> groupManager = GetItemsGroupManager(Application.Current.MainWindow);
            ItemsGroup group = groupManager.AddPublisher(itemsGroupSource);

            group.Bind();
        }
    }
}