using SoftCareManager.Common.UI.Region;
using SoftCareManager.Contracts.Application.Region;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SoftCareManager.Common.UI.Controls
{
    public class RegionControl : ContentControl
    {
        private IRegion region;

        public string RegionName
        {
            get { return (string)GetValue(RegionNameProperty); }
            set { SetValue(RegionNameProperty, value); }
        }

        public static readonly DependencyProperty RegionNameProperty =
            DependencyProperty.Register("RegionName", typeof(string), typeof(RegionControl), new PropertyMetadata(string.Empty, OnRegionNamePropertyChanged));

        private static void OnRegionNamePropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var regionControl = dependencyObject as RegionControl;
            if (regionControl == null
                || regionControl.region != null
                || regionControl.ShellId == -1)
            {
                return;
            }

            regionControl.AddToRegions(ReadRegionName(e.NewValue), regionControl.ShellId);
        }

        private static string ReadRegionName(object value)
        {
            var regionName = value as string;

            if (string.IsNullOrWhiteSpace(regionName))
            {
                throw new InvalidOperationException("RegionName is empty");
            }

            return regionName;
        }

        public int ShellId
        {
            get { return (int)GetValue(ShellIdProperty); }
            set { SetValue(ShellIdProperty, value); }
        }

        public static readonly DependencyProperty ShellIdProperty =
            DependencyProperty.Register("ShellId", typeof(int), typeof(RegionControl), new PropertyMetadata(-1, OnShellIdPropertyChanged));

        private static void OnShellIdPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var regionControl = dependencyObject as RegionControl;
            if (regionControl == null
                || regionControl.region != null
                || string.IsNullOrWhiteSpace(regionControl.RegionName))
            {
                return;
            }

            regionControl.AddToRegions(regionControl.RegionName, ReadShellId(e.NewValue));
        }

        private static int ReadShellId(object value)
        {
            var shellId = Convert.ToInt32(value);
            if (shellId < 0 || shellId >= 3)
            {
                throw new InvalidOperationException("ShellId is not valid.");
            }

            return shellId;
        }

        private void AddToRegions(string regionName, int shellId)
        {
            region = new Region.Region(regionName, shellId, this);
            var regionManager = RegionManager.GetRegionManager(this);
            regionManager.Regions.Add(region);
        }
    }
}