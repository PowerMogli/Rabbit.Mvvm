using SoftCareManager.Contracts.Application.Navigation;
using SoftCareManager.Contracts.General;

namespace SoftCareManager.Contracts.ViewModel
{
    public abstract class ViewModelBase : ObservableObject
    {
        protected bool IsInitialized;

        public bool CanBeActivated { get; protected set; }

        public virtual void Initialize(INavigationParameter navigationParameter)
        {
            if (IsInitialized)
            {
                return;
            }

            InnerInitialize(navigationParameter);
        }

        protected virtual void InnerInitialize(INavigationParameter parameter)
        {
        }
    }
}