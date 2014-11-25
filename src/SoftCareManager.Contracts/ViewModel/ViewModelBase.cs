using SoftCareManager.Contracts.Application.Navigation;
using SoftCareManager.Contracts.WorkItems;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace SoftCareManager.Contracts.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public bool CanBeActivated { get; protected set; }

        protected bool isInitialized;

        public virtual void Initialize(INavigationParameter navigationParameter)
        {
            if (isInitialized)
            {
                return;
            }

            InnerInitialize(navigationParameter);
        }

        protected virtual void InnerInitialize(INavigationParameter parameter)
        {

        }

        public virtual void SetProperty<T>(Expression<Func<T>> expression, ref T instanceField, T newValue)
        {
            if (!instanceField.Equals(null) && instanceField.Equals(newValue))
            {
                return;
            }

            var oldValue = instanceField;
            instanceField = newValue;

            var propertyName = GetPropertyName(expression);
            RaisePropertyChanged(propertyName);
        }

        protected void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string GetPropertyName<T>(Expression<Func<T>> propertyLambda)
        {
            var me = propertyLambda.Body as MemberExpression;

            if (me == null)
            {
                throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
            }

            return me.Member.Name;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}