using SoftCareManager.Contracts.Services;
using System;
using System.Windows;
using ServiceName = SoftCareManager.Contracts.General;
using Services = SoftCareManager.Contracts.Services;

namespace SoftCareManager.Common.UI.Service
{
    [Services.Service(ServiceName = ServiceName.Service.SkinService)]
    public class SkinService : ISkinService
    {
        public static readonly Uri DesktopUri;

        public static readonly Uri TouchUri;

        static SkinService()
        {
            DesktopUri = new Uri("pack://application:,,,/SoftCareManager.Views;component/Application/Desktop/AppResources.xaml", UriKind.RelativeOrAbsolute);

            TouchUri = new Uri("pack://application:,,,/SoftCareManager.Views;component/Application/Touch/AppResources.xaml", UriKind.RelativeOrAbsolute);
        }

        public BaseWindow MainWindow { get { return (BaseWindow)System.Windows.Application.Current.MainWindow; } }

        public void ChangeSkin()
        {
            MainWindow.Resources.MergedDictionaries.RemoveAt(0);

            MainWindow.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = MainWindow.IsTouch ? TouchUri : DesktopUri });
        }
    }
}