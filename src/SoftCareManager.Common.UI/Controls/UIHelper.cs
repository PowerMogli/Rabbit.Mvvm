using System.Windows;
using System.Windows.Media;

namespace SoftCareManager.Common.UI.Controls
{
    internal static class UIHelper
    {
        internal static T TryFindParent<T>(DependencyObject current) where T : class
        {
            DependencyObject parent = VisualTreeHelper.GetParent(current);
            if (parent == null)
                parent = LogicalTreeHelper.GetParent(current);
            if (parent == null)
                return null;

            if (parent is T)
                return parent as T;
            else
                return TryFindParent<T>(parent);
        }
    }
}