using SoftCareManager.Contracts.Application.Region;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows;

namespace SoftCareManager.Common.UI.Region
{
    [Export(typeof(IRegionManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class RegionManager : IRegionManager
    {
        public static EventHandler Initialized;

        public List<IRegion> Regions { get; set; }

        [ImportingConstructor]
        public RegionManager()
        {
            this.Regions = new List<IRegion>();
        }

        // Using a DependencyProperty as the backing store for ShellId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShellIdProperty =
            DependencyProperty.RegisterAttached("ShellId", typeof(int), typeof(RegionManager), new PropertyMetadata(0));

        public static readonly DependencyProperty RegionNameProperty = DependencyProperty.RegisterAttached(
              "RegionName",
              typeof(string),
              typeof(RegionManager),
              new PropertyMetadata(OnSetRegionNamePropertyChanged));

        private static void OnSetRegionNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (IsInDesignMode(d))
            {
                return;
            }

            var delayedRegionCreator = new DelayedRegionCreator(d);
            delayedRegionCreator.Create();
        }

        private static bool IsInDesignMode(DependencyObject element)
        {
            return DesignerProperties.GetIsInDesignMode(element);
        }

        public static void SetRegionName(DependencyObject regionTarget, string regionName)
        {
            if (regionTarget == null) throw new ArgumentNullException("regionTarget");
            regionTarget.SetValue(RegionNameProperty, regionName);
        }

        public static string GetRegionName(DependencyObject regionTarget)
        {
            if (regionTarget == null) throw new ArgumentNullException("regionTarget");
            return regionTarget.GetValue(RegionNameProperty) as string;
        }

        public static readonly DependencyProperty RegionManagerProperty =
            DependencyProperty.RegisterAttached("RegionManager", typeof(IRegionManager), typeof(RegionManager), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public static IRegionManager GetRegionManager(DependencyObject target)
        {
            if (target == null) throw new ArgumentNullException("target");
            return (IRegionManager)target.GetValue(RegionManagerProperty);
        }

        public static void SetRegionManager(DependencyObject target, IRegionManager value)
        {
            if (target == null) throw new ArgumentNullException("target");
            target.SetValue(RegionManagerProperty, value);
        }

        public void Update()
        {
            var handler = Initialized;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}