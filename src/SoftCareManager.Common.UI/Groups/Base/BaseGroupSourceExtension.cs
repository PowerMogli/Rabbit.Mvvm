using SoftCareManager.Contracts.Groups.Base;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Markup;

namespace SoftCareManager.Common.UI.Groups.Base
{
    public abstract class BaseGroupSourceExtension<TGroupSource> : MarkupExtension
        where TGroupSource : IGroupSource
    {
        protected Lazy<TGroupSource> _groupSource;
        protected DependencyObject _targetObject;
        protected object _dataContext;

        public string GroupName { get; set; }

        protected abstract void OnDataContextFound();

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            GetTargetObject(serviceProvider);

            Guard.ArgumentIsNotNull(GroupName, "GroupName");

            if (FindDataContext() == false)
            {
                SubscribeToDataContextChangedEvent();
            }

            return null;
        }

        private void GetTargetObject(IServiceProvider serviceProvider)
        {
            IProvideValueTarget target = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
            if (target == null)
            {
                throw new InvalidOperationException("IProvideValueTarget is not present!");
            }

            _targetObject = SafeCast.Cast<object, DependencyObject>(target.TargetObject);
        }

        private void DataContextChanged(object sender, System.EventArgs e)
        {
            if (FindDataContext())
            {
                UnsubscribeFromDataContextChangedEvent();
            }
        }

        private void SubscribeToDataContextChangedEvent()
        {
            DependencyPropertyDescriptor.FromProperty(FrameworkElement.DataContextProperty,
                _targetObject.GetType())
                .AddValueChanged(_targetObject, DataContextChanged);
        }

        private void UnsubscribeFromDataContextChangedEvent()
        {
            DependencyPropertyDescriptor.FromProperty(FrameworkElement.DataContextProperty,
                 _targetObject.GetType())
                .RemoveValueChanged(_targetObject, DataContextChanged);
        }

        private bool FindDataContext()
        {
            _dataContext = _targetObject.GetValue(FrameworkElement.DataContextProperty)
                ?? _targetObject.GetValue(FrameworkContentElement.DataContextProperty);

            if (_dataContext != null)
            {
                OnDataContextFound();
            }

            return _dataContext != null;
        }
    }
}