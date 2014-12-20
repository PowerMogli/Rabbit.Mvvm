using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows;

using SoftCareManager.Contracts.Application.Region;

namespace SoftCareManager.Common.UI.Region
{
    [Export(typeof (IRegionManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class RegionManager : IRegionManager
    {
        public static readonly DependencyProperty RegionManagerProperty =
            DependencyProperty.RegisterAttached("RegionManager", typeof (IRegionManager), typeof (RegionManager), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty RegionNameProperty = DependencyProperty.RegisterAttached("RegionName",
                                                                                                           typeof (string),
                                                                                                           typeof (RegionManager),
                                                                                                           new PropertyMetadata(OnSetRegionNamePropertyChanged));

        public static readonly DependencyProperty ShellIdProperty =
            DependencyProperty.RegisterAttached("ShellId", typeof (int), typeof (RegionManager), new PropertyMetadata(0));

        public static EventHandler Initialized;

        [ImportingConstructor]
        public RegionManager()
        {
            Regions = new List<IRegion>();
        }

        public List<IRegion> Regions { get; set; }

        public static IRegionManager GetRegionManager(DependencyObject target)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }
            return (IRegionManager)target.GetValue(RegionManagerProperty);
        }

        public static string GetRegionName(DependencyObject regionTarget)
        {
            if (regionTarget == null)
            {
                throw new ArgumentNullException("regionTarget");
            }
            return regionTarget.GetValue(RegionNameProperty) as string;
        }

        public static void SetRegionManager(DependencyObject target, IRegionManager value)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }
            target.SetValue(RegionManagerProperty, value);
        }

        public static void SetRegionName(DependencyObject regionTarget, string regionName)
        {
            if (regionTarget == null)
            {
                throw new ArgumentNullException("regionTarget");
            }
            regionTarget.SetValue(RegionNameProperty, regionName);
        }

        public void Update()
        {
            EventHandler handler = Initialized;
            if (handler != null)
            {
                handler(this, System.EventArgs.Empty);
            }
        }

        private static bool IsInDesignMode(DependencyObject element)
        {
            return DesignerProperties.GetIsInDesignMode(element);
        }

        private static void OnSetRegionNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (IsInDesignMode(d))
            {
                return;
            }

            DelayedRegionCreator delayedRegionCreator = new DelayedRegionCreator(d);
            delayedRegionCreator.Create();
        }
    }
}