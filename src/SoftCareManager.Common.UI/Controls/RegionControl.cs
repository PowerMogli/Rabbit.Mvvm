using System;
using System.Windows;
using System.Windows.Controls;

using SoftCareManager.Common.UI.Region;
using SoftCareManager.Contracts.Application.Region;

namespace SoftCareManager.Common.UI.Controls
{
    public class RegionControl : ContentControl
    {
        public static readonly DependencyProperty RegionNameProperty =
            DependencyProperty.Register("RegionName", typeof (string), typeof (RegionControl), new PropertyMetadata(string.Empty, OnRegionNamePropertyChanged));

        public static readonly DependencyProperty ShellIdProperty =
            DependencyProperty.Register("ShellId", typeof (int), typeof (RegionControl), new PropertyMetadata(-1, OnShellIdPropertyChanged));

        private IRegion region;

        public string RegionName
        {
            get { return (string)GetValue(RegionNameProperty); }
            set { SetValue(RegionNameProperty, value); }
        }

        public int ShellId
        {
            get { return (int)GetValue(ShellIdProperty); }
            set { SetValue(ShellIdProperty, value); }
        }

        private static void OnRegionNamePropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            RegionControl regionControl = dependencyObject as RegionControl;
            if (regionControl == null
                || regionControl.region != null
                || regionControl.ShellId == -1)
            {
                return;
            }

            regionControl.AddToRegions(ReadRegionName(e.NewValue), regionControl.ShellId);
        }

        private static void OnShellIdPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            RegionControl regionControl = dependencyObject as RegionControl;
            if (regionControl == null
                || regionControl.region != null
                || string.IsNullOrWhiteSpace(regionControl.RegionName))
            {
                return;
            }

            regionControl.AddToRegions(regionControl.RegionName, ReadShellId(e.NewValue));
        }

        private static string ReadRegionName(object value)
        {
            string regionName = value as string;

            if (string.IsNullOrWhiteSpace(regionName))
            {
                throw new InvalidOperationException("RegionName is empty");
            }

            return regionName;
        }

        private static int ReadShellId(object value)
        {
            int shellId = Convert.ToInt32(value);
            if (shellId < 0 || shellId >= 3)
            {
                throw new InvalidOperationException("ShellId is not valid.");
            }

            return shellId;
        }

        private void AddToRegions(string regionName, int shellId)
        {
            region = new Region.Region(regionName, shellId, this);
            IRegionManager regionManager = RegionManager.GetRegionManager(this);
            regionManager.Regions.Add(region);
        }
    }
}