using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace SoftCareManager.Common.UI
{
    public abstract class BaseWindow : Window
    {
        public event EventHandler IsTouchChanging = delegate { };

        public event EventHandler ShellIdChanging = delegate { };

        public bool IsTouch
        {
            get { return (bool)GetValue(IsTouchProperty); }
            set { SetValue(IsTouchProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Shell. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsTouchProperty =
            DependencyProperty.Register("IsTouch", typeof(bool), typeof(BaseWindow), new PropertyMetadata(false, OnIsTouchPropertyChanged));

        private static void OnIsTouchPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var baseWindow = dependencyObject as BaseWindow;
            baseWindow.IsTouchChanging(baseWindow, EventArgs.Empty);
        }

        public ContentControl Shell
        {
            get { return (ContentControl)GetValue(ShellProperty); }
            set { SetValue(ShellProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Shell.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShellProperty =
            DependencyProperty.Register("Shell", typeof(ContentControl), typeof(BaseWindow));

        public int ShellId
        {
            get { return (int)GetValue(ShellIdProperty); }
            set { SetValue(ShellIdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShellId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShellIdProperty =
            DependencyProperty.Register("ShellId", typeof(int), typeof(BaseWindow), new PropertyMetadata(0, OnShellIdPropertyChanged));

        private static void OnShellIdPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var baseWindow = dependencyObject as BaseWindow;
            baseWindow.ShellIdChanging(baseWindow, EventArgs.Empty);
        }

        public ICommand NavigateToCommand
        {
            get { return (ICommand)GetValue(NavigateToCommandProperty); }
            set { SetValue(NavigateToCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NavigateToCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NavigateToCommandProperty =
            DependencyProperty.Register("NavigateToCommand", typeof(ICommand), typeof(BaseWindow));

        public ICommand ChangeSkin
        {
            get { return (ICommand)GetValue(ChangeSkinProperty); }
            set { SetValue(ChangeSkinProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ChangeSkin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChangeSkinProperty =
            DependencyProperty.Register("ChangeSkin", typeof(ICommand), typeof(BaseWindow));

        public ICommand LoadedCommand
        {
            get { return (ICommand)GetValue(LoadedCommandProperty); }
            set { SetValue(LoadedCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LoadedCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoadedCommandProperty =
            DependencyProperty.Register("LoadedCommand", typeof(ICommand), typeof(BaseWindow));
    }
}