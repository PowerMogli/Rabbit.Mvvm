﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using SoftCareManager.Contracts.ViewModel;
using SoftCareManager.Views.Application.Desktop;

namespace SoftCareManager.Views.Application.Behavior
{
    public class DataContextBehavior
    {
        public static readonly DependencyProperty DataContextBehaviorProperty =
            DependencyProperty.RegisterAttached("DataContextBehavior", typeof (DataContextBehavior), typeof (DataContextBehavior), new PropertyMetadata(null));

        public static readonly DependencyProperty IsTouchProperty =
            DependencyProperty.RegisterAttached("IsTouch", typeof (bool), typeof (DataContextBehavior), new PropertyMetadata(false, OnIsTouchPropertyChanged));

        public static readonly DependencyProperty MonitorShellProperty =
            DependencyProperty.RegisterAttached("MonitorShell", typeof (bool), typeof (DataContextBehavior), new PropertyMetadata(false, OnMonitorShellPropertyChanged));

        private readonly List<Lazy<UserControl>> _desktopShellViews;
        private readonly ContentControl _shellControl;
        private readonly List<Lazy<UserControl>> _touchShellViews;
        private bool _isTouch;
        private int _shellId;

        public DataContextBehavior(ContentControl contentControl)
        {
            _shellControl = contentControl;

            _desktopShellViews = new List<Lazy<UserControl>>
            {
                new Lazy<UserControl>(() => new AppShellView()),
                new Lazy<UserControl>(() => new AppShellView()),
                new Lazy<UserControl>(() => new AppShellView())
            };

            _touchShellViews = new List<Lazy<UserControl>>
            {
                new Lazy<UserControl>(() => new Touch.AppShellView()),
                new Lazy<UserControl>(() => new Touch.AppShellView()),
                new Lazy<UserControl>(() => new Touch.AppShellView())
            };
        }

        public static DataContextBehavior GetDataContextBehavior(DependencyObject obj)
        {
            return (DataContextBehavior)obj.GetValue(DataContextBehaviorProperty);
        }

        public static bool GetIsTouch(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsTouchProperty);
        }

        public static bool GetMonitorShell(DependencyObject obj)
        {
            return (bool)obj.GetValue(MonitorShellProperty);
        }

        public static void SetDataContextBehavior(DependencyObject obj, DataContextBehavior value)
        {
            obj.SetValue(DataContextBehaviorProperty, value);
        }

        public static void SetIsTouch(DependencyObject obj, bool value)
        {
            obj.SetValue(IsTouchProperty, value);
        }

        public static void SetMonitorShell(DependencyObject obj, bool value)
        {
            obj.SetValue(MonitorShellProperty, value);
        }

        private static void OnIsTouchPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            DataContextBehavior dataContextBehavior = GetDataContextBehavior(dependencyObject);
            if (dataContextBehavior == null)
            {
                return;
            }

            dataContextBehavior._isTouch = (bool)e.NewValue;
            dataContextBehavior.SetContent();
        }

        // Using a DependencyProperty as the backing store for MonitorShell.  This enables animation, styling, binding, etc...

        private static void OnMonitorShellPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            DataContextBehavior dataContextBehavior = new DataContextBehavior(dependencyObject as ContentControl);

            SetDataContextBehavior(dependencyObject, dataContextBehavior);

            dataContextBehavior.Attach();
        }

        private void Attach()
        {
            _shellControl.DataContextChanged -= OnDataContextChanged;
            _shellControl.DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            IShellAware shellAware = e.NewValue as IShellAware;
            if (shellAware == null)
            {
                return;
            }

            _shellId = shellAware.ShellId;

            SetContent();
        }

        private void SetContent()
        {
            _shellControl.Content = _isTouch
                ? _touchShellViews[_shellId].Value
                : _desktopShellViews[_shellId].Value;
        }
    }
}