using System.Windows;

namespace SoftCareManager.Common.UI.Controls
{
    public static class ShellIdReader
    {
        public static int Read(DependencyObject dependencyObject)
        {
            RegionControl regionControl = dependencyObject as RegionControl;
            if (regionControl != null)
            {
                return regionControl.ShellId;
            }

            DependencyObject parent = UIHelper.TryFindParent<RegionControl>(dependencyObject);
            return parent != null
                ? Read(parent)
                : -1;
        }
    }
}