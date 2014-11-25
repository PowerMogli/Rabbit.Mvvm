using SoftCareManager.Contracts.General;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SoftCareManager.Common.UI.Templates
{
    public class AppDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
            {
                return null;
            }

            var name = item.GetType().Name;
            name = name.Substring(0, name.IndexOf("Model"));

            var frameworkElement = container as FrameworkElement;

            if (name != null)
            {
                switch (name)
                {
                    case Regions.AppInformationView:
                    case Regions.AppMenuView:
                    case Regions.AppShellView:
                        return GetTemplate(frameworkElement, name);
                    default:
                        throw new InvalidOperationException();
                }
            }

            return null;
        }

        private DataTemplate GetTemplate(FrameworkElement frameworkElement, string templateName)
        {
            var template = frameworkElement.TryFindResource(templateName);

            return template as DataTemplate;
        }
    }
}