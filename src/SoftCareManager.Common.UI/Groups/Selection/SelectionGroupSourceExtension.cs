using SoftCareManager.Contracts.Groups.Selection;
using System;
using System.Windows;
using System.Windows.Markup;

namespace SoftCareManager.Common.UI.Groups.Selection
{
    [MarkupExtensionReturnType(typeof(SelectionGroupSource))]
    public class SelectionGroupSourceExtension : MarkupExtension
    {
        public string SelectionGroupName { get; set; }

        public ISelectionPublisher SelectionPublisher { get; private set; }

        public SelectionGroupSourceExtension() { }

        public SelectionGroupSourceExtension(string selectionGroupName)
        {
            SelectionGroupName = selectionGroupName;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            SelectionPublisher = GetDataContext(serviceProvider);

            if (string.IsNullOrWhiteSpace(SelectionGroupName)
                || SelectionPublisher == null)
            {
                throw new InvalidOperationException("SelectionGroupName and DataContext  must be provided.");
            }

            return new SelectionGroupSource(SelectionGroupName, SelectionPublisher);
        }

        private static ISelectionPublisher GetDataContext(IServiceProvider serviceProvider)
        {
            var target = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
            if (target == null)
            {
                return null;
            }

            var dependencyObject = target.TargetObject as DependencyObject;
            if (dependencyObject == null)
            {
                return null;
            }

            return dependencyObject.GetValue(FrameworkElement.DataContextProperty) as ISelectionPublisher;
        }
    }
}