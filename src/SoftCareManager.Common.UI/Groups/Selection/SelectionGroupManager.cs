using SoftCareManager.Common.UI.Groups.Base;
using System.ComponentModel.Composition;
using System.Windows;

namespace SoftCareManager.Common.UI.Groups.Selection
{
    [Export(typeof(ISelectionGroupManager))]
    public class SelectionGroupManager : BaseGroupManager<SelectionGroup, SelectionGroupSource>, ISelectionGroupManager
    {
        public static ISelectionGroupManager GetSelectionGroupManager(DependencyObject obj)
        {
            return (ISelectionGroupManager)obj.GetValue(SelectionGroupManagerProperty);
        }

        public static void SetSelectionGroupManager(DependencyObject obj, ISelectionGroupManager value)
        {
            obj.SetValue(SelectionGroupManagerProperty, value);
        }

        public static readonly DependencyProperty SelectionGroupManagerProperty =
            DependencyProperty.RegisterAttached("SelectionGroupManager", typeof(ISelectionGroupManager), typeof(SelectionGroupManager), new PropertyMetadata(null));

        public static string GetSelectionGroupName(DependencyObject obj)
        {
            return (string)obj.GetValue(SelectionGroupNameProperty);
        }

        public static void SetSelectionGroupName(DependencyObject obj, string value)
        {
            obj.SetValue(SelectionGroupNameProperty, value);
        }

        public static readonly DependencyProperty SelectionGroupNameProperty =
            DependencyProperty.RegisterAttached("SelectionGroupName", typeof(string), typeof(SelectionGroupManager), new PropertyMetadata(string.Empty, OnSelectionGroupNamePropertyChanged));

        private static void OnSelectionGroupNamePropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var selectionGroupManager = GetSelectionGroupManager(Application.Current.MainWindow);
            var selectionGroup = selectionGroupManager.AddSubscriber(e.NewValue as string, dependencyObject);

            selectionGroup.Bind();
        }

        public static SelectionGroupSource GetSelectionGroupSource(DependencyObject obj)
        {
            return (SelectionGroupSource)obj.GetValue(SelectionGroupSourceProperty);
        }

        public static void SetSelectionGroupSource(DependencyObject obj, SelectionGroupSource value)
        {
            obj.SetValue(SelectionGroupSourceProperty, value);
        }

        public static readonly DependencyProperty SelectionGroupSourceProperty =
            DependencyProperty.RegisterAttached("SelectionGroupSource", typeof(SelectionGroupSource), typeof(SelectionGroupManager), new PropertyMetadata(null, OnSelectionGroupSourcePropertyChanged));

        private static void OnSelectionGroupSourcePropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var selectionGroupSource = e.NewValue as SelectionGroupSource;
            if (selectionGroupSource == null)
            {
                return;
            }

            var selectionGroupManager = GetSelectionGroupManager(Application.Current.MainWindow);
            var selectionGroup = selectionGroupManager.AddPublisher(selectionGroupSource);

            selectionGroup.Bind();
        }
    }
}