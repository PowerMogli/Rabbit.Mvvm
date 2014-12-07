using System.Windows;
using System.Windows.Controls;

using SoftCareManager.Contracts.Application.Region;

namespace SoftCareManager.Common.UI.Region
{
    public class RegionManagerRegistrator
    {
        public IRegionManager FindRegionManager(DependencyObject dependencyObject)
        {
            IRegionManager regionmanager = RegionManager.GetRegionManager(dependencyObject);
            if (regionmanager != null)
            {
                return regionmanager;
            }

            DependencyObject parent = LogicalTreeHelper.GetParent(dependencyObject);
            return parent != null
                ? FindRegionManager(parent)
                : null;
        }

        public void Register(Region region, ContentControl contentControl)
        {
            IRegionManager regionManager = FindRegionManager(contentControl);
            regionManager.Regions.Add(region);
        }
    }
}