using System;
using System.Windows;
using System.Windows.Controls;

namespace SoftCareManager.Common.UI.Region
{
    internal class DelayedRegionCreator
    {
        private WeakReference elementWeakReference;
        private bool regionCreated;

        /// <summary>
        /// The element that will host the Region. 
        /// </summary>
        /// <value>The target element.</value>
        public DependencyObject TargetElement
        {
            get { return this.elementWeakReference != null ? this.elementWeakReference.Target as DependencyObject : null; }
            set { this.elementWeakReference = new WeakReference(value); }
        }

        public DelayedRegionCreator(DependencyObject targetElement)
        {
            TargetElement = targetElement;
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
            this.UnWireTargetElement();
            this.TryCreateRegion();
        }

        private void WireUpTargetElement()
        {
            FrameworkElement element = this.TargetElement as FrameworkElement;
            if (element != null)
            {
                element.Loaded += this.ElementLoaded;
            }
        }

        private void UnWireTargetElement()
        {
            FrameworkElement element = this.TargetElement as FrameworkElement;
            if (element != null)
            {
                element.Loaded -= this.ElementLoaded;
            }
        }

        private void TryCreateRegion()
        {
            DependencyObject targetElement = this.TargetElement;
            if (targetElement == null)
            {
                return;
            }

            if (targetElement.CheckAccess())
            {
                if (this.regionCreated)
                {
                    return;
                }

                var regionName = RegionManager.GetRegionName(TargetElement);
                var region = new Region(regionName, -1, TargetElement as ContentControl);

                var regionManagerRegistrator = new RegionManagerRegistrator();
                regionManagerRegistrator.Register(region, TargetElement as ContentControl);
                this.regionCreated = true;
            }
        }
    }
}