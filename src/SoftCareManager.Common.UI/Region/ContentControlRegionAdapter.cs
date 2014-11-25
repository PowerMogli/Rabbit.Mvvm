using System;
using System.Windows.Controls;

namespace SoftCareManager.Common.UI.Region
{
    internal class ContentControlRegionAdapter
    {
        internal void Initialize(string regionName, ContentControl hostControl)
        {
            if (hostControl == null)
            {
                throw new InvalidOperationException("Hostcontrol is not a ContentControl - only ContentControl is allowed to be a region.");
            }

            //var region = new Region();
            //region.Name = regionName;

            //var regionManagerRegistrator = new RegionManagerRegistrator();
            //regionManagerRegistrator.Register(region, hostControl);
        }
    }
}