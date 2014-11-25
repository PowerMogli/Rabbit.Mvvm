using SoftCareManager.Contracts.Application.Region;
using System.Windows;
using System.Windows.Controls;

namespace SoftCareManager.Common.UI.Region
{
    public class RegionManagerRegistrator
    {
        public void Register(Region region, ContentControl contentControl)
        {
            var regionManager = FindRegionManager(contentControl);
            regionManager.Regions.Add(region);
        }

        public IRegionManager FindRegionManager(DependencyObject dependencyObject)
        {
            var regionmanager = RegionManager.GetRegionManager(dependencyObject);
            if (regionmanager != null)
            {
                return regionmanager;
            }

            DependencyObject parent = null;
            parent = LogicalTreeHelper.GetParent(dependencyObject);
            if (parent != null)
            {
                return this.FindRegionManager(parent);
            }

            return null;
        }
    }
}