using System;
using System.Windows;
using SoftCareManager.Contracts.Services;

namespace SoftCareManager.Common.UI.Services
{
    [Service(ServiceName = Contracts.General.Service.SkinService)]
    public class SkinService : ISkinService
    {
        public static readonly Uri DesktopUri;

        public static readonly Uri TouchUri;

        static SkinService()
        {
            DesktopUri = new Uri("pack://application:,,,/SoftCareManager.Views;component/Application/Desktop/AppResources.xaml", UriKind.RelativeOrAbsolute);

            TouchUri = new Uri("pack://application:,,,/SoftCareManager.Views;component/Application/Touch/AppResources.xaml", UriKind.RelativeOrAbsolute);
        }

        public Window MainWindow { get { return Application.Current.MainWindow; } }

        public void ChangeSkin(bool isTouch)
        {
            MainWindow.Resources.MergedDictionaries.RemoveAt(0);

            MainWindow.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = isTouch ? TouchUri : DesktopUri });
        }
    }
}