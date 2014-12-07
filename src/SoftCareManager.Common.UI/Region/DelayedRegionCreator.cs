using System;
using System.Windows;
using System.Windows.Controls;

namespace SoftCareManager.Common.UI.Region
{
    internal class DelayedRegionCreator
    {
        private WeakReference _elementWeakReference;
        private bool _regionCreated;

        public DelayedRegionCreator(DependencyObject targetElement)
        {
            TargetElement = targetElement;
        }

        /// <summary>
        /// The element that will host the Region. 
        /// </summary>
        /// <value>The target element.</value>
        public DependencyObject TargetElement
        {
            get
            {
                return _elementWeakReference != null
                    ? _elementWeakReference.Target as DependencyObject
                    : null;
            }
            set { _elementWeakReference = new WeakReference(value); }
        }

        internal void Create()
        {
            EventHandler onInitialized = null;
            onInitialized = (s, e) =>
            {
                RegionManager.Initialized -= onInitialized;
                TryCreateRegion();
            };

            RegionManager.Initialized += onInitialized;
        }

        private void ElementLoaded(object sender, RoutedEventArgs e)
        {
            UnWireTargetElement();
            TryCreateRegion();
        }

        private void TryCreateRegion()
        {
            DependencyObject targetElement = TargetElement;

            if (targetElement == null
                || !targetElement.CheckAccess()
                || _regionCreated)
            {
                return;
            }

            string regionName = RegionManager.GetRegionName(TargetElement);
            Region region = new Region(regionName, -1, TargetElement as ContentControl);

            RegionManagerRegistrator regionManagerRegistrator = new RegionManagerRegistrator();
            regionManagerRegistrator.Register(region, TargetElement as ContentControl);
            _regionCreated = true;
        }

        private void UnWireTargetElement()
        {
            FrameworkElement element = TargetElement as FrameworkElement;
            if (element != null)
            {
                element.Loaded -= ElementLoaded;
            }
        }

        private void WireUpTargetElement()
        {
            FrameworkElement element = TargetElement as FrameworkElement;
            if (element != null)
            {
                element.Loaded += ElementLoaded;
            }
        }
    }
}