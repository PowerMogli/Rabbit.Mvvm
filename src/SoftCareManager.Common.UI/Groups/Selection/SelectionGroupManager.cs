using System.ComponentModel.Composition;
using System.Windows;

using SoftCareManager.Common.UI.Groups.Base;

namespace SoftCareManager.Common.UI.Groups.Selection
{
    [Export(typeof (IBaseGroupManager<SelectionGroup, SelectionGroupSource>))]
    public class SelectionGroupManager : BaseGroupManager<SelectionGroup, SelectionGroupSource>
    {
        public static readonly DependencyProperty SelectionGroupManagerProperty =
            DependencyProperty.RegisterAttached("SelectionGroupManager", typeof (IBaseGroupManager<SelectionGroup, SelectionGroupSource>), typeof (SelectionGroupManager), new PropertyMetadata(null));

        public static readonly DependencyProperty SelectionGroupNameProperty =
            DependencyProperty.RegisterAttached("SelectionGroupName", typeof (string), typeof (SelectionGroupManager), new PropertyMetadata(string.Empty, OnSelectionGroupNamePropertyChanged));

        public static readonly DependencyProperty SelectionGroupSourceProperty =
            DependencyProperty.RegisterAttached("SelectionGroupSource", typeof (SelectionGroupSource), typeof (SelectionGroupManager), new PropertyMetadata(null, OnSelectionGroupSourcePropertyChanged));

        public static IBaseGroupManager<SelectionGroup, SelectionGroupSource> GetSelectionGroupManager(DependencyObject obj)
        {
            return (IBaseGroupManager<SelectionGroup, SelectionGroupSource>)obj.GetValue(SelectionGroupManagerProperty);
        }

        public static string GetSelectionGroupName(DependencyObject obj)
        {
            return (string)obj.GetValue(SelectionGroupNameProperty);
        }

        public static SelectionGroupSource GetSelectionGroupSource(DependencyObject obj)
        {
            return (SelectionGroupSource)obj.GetValue(SelectionGroupSourceProperty);
        }

        public static void SetSelectionGroupManager(DependencyObject obj, IBaseGroupManager<SelectionGroup, SelectionGroupSource> value)
        {
            obj.SetValue(SelectionGroupManagerProperty, value);
        }

        public static void SetSelectionGroupName(DependencyObject obj, string value)
        {
            obj.SetValue(SelectionGroupNameProperty, value);
        }

        public static void SetSelectionGroupSource(DependencyObject obj, SelectionGroupSource value)
        {
            obj.SetValue(SelectionGroupSourceProperty, value);
        }

        private static void OnSelectionGroupNamePropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            IBaseGroupManager<SelectionGroup, SelectionGroupSource> groupManager = GetSelectionGroupManager(Application.Current.MainWindow);
            SelectionGroup group = groupManager.AddSubscriber(e.NewValue as string, dependencyObject);

            group.Bind();
        }

        private static void OnSelectionGroupSourcePropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            SelectionGroupSource selectionGroupSource = e.NewValue as SelectionGroupSource;
            if (selectionGroupSource == null)
            {
                return;
            }

            IBaseGroupManager<SelectionGroup, SelectionGroupSource> groupManager = GetSelectionGroupManager(Application.Current.MainWindow);
            SelectionGroup group = groupManager.AddPublisher(selectionGroupSource);

            group.Bind();
        }
    }
}