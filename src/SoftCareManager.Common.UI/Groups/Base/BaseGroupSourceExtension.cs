using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Markup;

using SoftCareManager.Contracts.Groups.Base;

namespace SoftCareManager.Common.UI.Groups.Base
{
    public abstract class BaseGroupSourceExtension<TGroupSource> : MarkupExtension
        where TGroupSource : IGroupSource
    {
        protected object DataContext;
        protected Lazy<TGroupSource> GroupSource;
        protected DependencyObject TargetObject;

        protected BaseGroupSourceExtension(string groupName)
        {
            GroupName = groupName;
        }

        public string GroupName { get; set; }

        public override sealed object ProvideValue(IServiceProvider serviceProvider)
        {
            GetTargetObject(serviceProvider);

            if (string.IsNullOrWhiteSpace(GroupName))
            {
                throw new InvalidOperationException("SelectionGroupName must be provided.");
            }

            if (FindDataContext())
            {
                return GroupSource.Value;
            }

            SubscribeToDataContextChangedEvent();
            return null;
        }

        protected abstract void OnDataContextFound();

        private void DataContextChanged(object sender, System.EventArgs e)
        {
            if (FindDataContext() == false)
            {
                return;
            }

            OnDataContextFound();
            UnsubscribeFromDataContextChangedEvent();
        }

        private bool FindDataContext()
        {
            DataContext = TargetObject.GetValue(FrameworkElement.DataContextProperty)
                          ?? TargetObject.GetValue(FrameworkContentElement.DataContextProperty);

            return DataContext != null;
        }

        private void GetTargetObject(IServiceProvider serviceProvider)
        {
            IProvideValueTarget target = (IProvideValueTarget)serviceProvider.GetService(typeof (IProvideValueTarget));
            if (target == null)
            {
                throw new InvalidOperationException("IProvideValueTarget is not present!");
            }

            TargetObject = target.TargetObject as DependencyObject;
            if (TargetObject == null)
            {
                throw new InvalidOperationException("TargetObject is not a valid DependencyObject");
            }
        }

        private void SubscribeToDataContextChangedEvent()
        {
            DependencyPropertyDescriptor.FromProperty(FrameworkElement.DataContextProperty,
                                                      TargetObject.GetType())
                                        .AddValueChanged(TargetObject, DataContextChanged);
        }

        private void UnsubscribeFromDataContextChangedEvent()
        {
            DependencyPropertyDescriptor.FromProperty(FrameworkElement.DataContextProperty,
                                                      TargetObject.GetType())
                                        .RemoveValueChanged(TargetObject, DataContextChanged);
        }
    }
}