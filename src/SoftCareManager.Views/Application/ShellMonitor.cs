using SoftCareManager.Common.UI;
using SoftCareManager.ViewModel.Application;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SoftCareManager.Views.Application
{
    public class ShellMonitor
    {
        private BaseWindow _window;

        public static bool GetShellMonitor(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShellMonitorProperty);
        }

        public static void SetShellMonitor(DependencyObject obj, bool value)
        {
            obj.SetValue(ShellMonitorProperty, value);
        }

        // Using a DependencyProperty as the backing store for MonitorPlatformChange.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShellMonitorProperty =
            DependencyProperty.RegisterAttached("ShellMonitor", typeof(bool), typeof(ShellMonitor), new PropertyMetadata(false, OnShellMonitorPropertyChanged));

        private static void OnShellMonitorPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var shellMonitor = new ShellMonitor(dependencyObject as BaseWindow);
            shellMonitor.Attach();
        }

        List<Lazy<ContentControl>> desktopShellViews;
        List<Lazy<ContentControl>> touchShellViews;

        public ShellMonitor(BaseWindow window)
        {
            _window = window;

            desktopShellViews = new List<Lazy<ContentControl>>
            {
                new Lazy<ContentControl>(() => new Desktop.AppShellView { DataContext = new AppShellViewModel(0) }),
                new Lazy<ContentControl>(() => new Desktop.AppShellView { DataContext = new AppShellViewModel(1) }),
                new Lazy<ContentControl>(() => new Desktop.AppShellView { DataContext = new AppShellViewModel(2) })
            };

            touchShellViews = new List<Lazy<ContentControl>>
            {
                new Lazy<ContentControl>(() => new Touch.AppShellView { DataContext = new AppShellViewModel(0) }),
                new Lazy<ContentControl>(() => new Touch.AppShellView { DataContext = new AppShellViewModel(1) }),
                new Lazy<ContentControl>(() => new Touch.AppShellView { DataContext = new AppShellViewModel(2) })
            };
        }

        private void Attach()
        {
            _window.Shell = desktopShellViews[0].Value;

            _window.ShellIdChanging -= OnIsShellIdChanging;
            _window.ShellIdChanging += OnIsShellIdChanging;
        }

        private void OnIsShellIdChanging(object sender, EventArgs e)
        {
            _window.Shell = _window.IsTouch
                ? touchShellViews[_window.ShellId].Value
                : desktopShellViews[_window.ShellId].Value;
        }
    }
}